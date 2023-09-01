using EcommerceAPI.Controllers;
using EcommerceAPI.Models;
using EcommerceAPI.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EcommerceTests.Auth
{
    public class AuthUserTests
    {
        #region Register

        [Fact]
        public async Task Register_ReturnsOkResult_WhenModelIsValid()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,

                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            var mockConfig = new Mock<IConfiguration>();

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new RegisterModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                FullName = "Test User",
                Address = "123 Test St",
                BirthDate = new DateTime(2000, 1, 1)
            };

            var result = await controller.Register(model);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsBadRequestResult_WhenModelIsInvalid()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Test Error" }));

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,

                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            var mockConfig = new Mock<IConfiguration>();

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new RegisterModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                FullName = "Test User",
                Address = "123 Test St",
                BirthDate = new DateTime(2000, 1, 1)
            };

            var result = await controller.Register(model);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Test Error", ((IEnumerable<IdentityError>)badRequestResult.Value).First().Description);
        }

        [Fact]
        public async Task Register_ReturnsBadRequestResult_WhenEmailIsEmpty()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            var mockConfig = new Mock<IConfiguration>();

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new RegisterModel
            {
                Email = "",
                Password = "Test123!",
                FullName = "Test User",
                Address = "123 Test St",
                BirthDate = new DateTime(2000, 1, 1)
            };

            var result = await controller.Register(model);

            Assert.IsType<BadRequestResult>(result);
        }

        #endregion Register

        #region Login

        [Fact]
        public async Task Login_ReturnsOkResult_WhenModelIsValid()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { UserName = "test@example.com" });

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,

                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            mockSignInManager.Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var mockConfig = new Mock<IConfiguration>();

            mockConfig.Setup(x => x["Jwt:Key"]).Returns("a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6"); // fake key

            mockConfig.Setup(x => x["Jwt:Issuer"]).Returns("testissuer");

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "Test123!"
            };

            var result = await controller.Login(model);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WhenModelIsInvalid()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,

                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            mockSignInManager.Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var mockConfig = new Mock<IConfiguration>();

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "Test123!"
            };

            var result = await controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WhenCredentialsAreInvalid()
        {
            var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null);

            mockSignInManager.Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var mockConfig = new Mock<IConfiguration>();

            var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "InvalidPassword"
            };

            var result = await controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        //[Fact]
        //public void GenerateToken_ReturnsValidToken_WhenUserIsValid()
        //{
        //    var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(),
        //        null, null, null, null, null, null, null, null);

        //    var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object,
        //        Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
        //        null, null, null, null);

        //    var mockConfig = new Mock<IConfiguration>();

        //    mockConfig.Setup(x => x["Jwt:Key"]).Returns("a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6");
        //    mockConfig.Setup(x => x["Jwt:Issuer"]).Returns("testissuer");

        //    var controller = new AuthController(mockUserManager.Object, mockSignInManager.Object, mockConfig.Object);

        //    var user = new User { UserName = "test@example.com" };

        //    var tokenString = controller.GenerateToken(user);

        //    Assert.NotNull(tokenString);
        //}

        #endregion Login
    }
}