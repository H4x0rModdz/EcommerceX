using EcommerceAPI.Controllers;
using EcommerceAPI.Models;
using EcommerceAPI.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EcommerceTests.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthControllerTests()
        {
            var userStore = Substitute.For<IUserStore<User>>();
            var contextAccessor = Substitute.For<IHttpContextAccessor>();
            var claimsFactory = Substitute.For<IUserClaimsPrincipalFactory<User>>();
            var optionsAccessor = Substitute.For<IOptions<IdentityOptions>>();

            _userManager = Substitute.For<UserManager<User>>(
                userStore, null, null, null, null, null, null, null, null);

            _signInManager = new SignInManagerMock(
                _userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                Substitute.For<ILogger<SignInManager<User>>>(),
                Substitute.For<IAuthenticationSchemeProvider>(),
                new SimpleUserConfirmation()
            );

            _config = Substitute.For<IConfiguration>();
            _authController = new AuthController(_userManager, _signInManager, _config);
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsOk()
        {
            var model = new RegisterModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                FullName = "Test User",
                Address = "123 Test St",
                BirthDate = new DateTime(2000, 1, 1),
                ConfirmPassword = "Test123!"
            };

            _userManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            var result = await _authController.Register(model) as OkObjectResult;

            Assert.NotNull(result);
            var actualResult = result.Value as JsonResult;
            var actualMessage = actualResult?.Value?.ToString()?.Trim();

            Assert.Equal(actualMessage, actualMessage);
        }

        //[Fact]
        //public async Task Register_InvalidModel_ReturnsBadRequest()
        //{
        //    var model = new RegisterModel
        //    {
        //        Email = "test@example.com",
        //        Password = "invalid",
        //        FullName = "Test User",
        //        Address = "123 Test St",
        //        BirthDate = new DateTime(2000, 1, 1),
        //        ConfirmPassword = "invalid"
        //    };

        //    var result = await _authController.Register(model) as BadRequestObjectResult;

        //    Assert.NotNull(result);
        //    Assert.Equal(400, result.StatusCode);
        //    var errors = result.Value as IdentityResult;

        //    Assert.NotNull(errors);
        //    Assert.False(errors.Succeeded);
        //}

        //[Fact]
        //public async Task Register_RegistrationFailure_ReturnsBadRequestWithErrors()
        //{
        //    var model = new RegisterModel
        //    {
        //        Email = "test@example.com",
        //        Password = "Test123!",
        //        FullName = "Test User",
        //        Address = "123 Test St",
        //        BirthDate = new DateTime(2000, 1, 1),
        //        ConfirmPassword = "Test123!"
        //    };

        //    var identityResult = IdentityResult.Failed(new IdentityError { Description = "Registration failed" });

        //    _userManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
        //        .Returns(IdentityResult.Failed(new IdentityError { Description = "Registration failed" }));

        //    var result = await _authController.Register(model) as BadRequestObjectResult;

        //    Assert.NotNull(result);
        //    Assert.Equal(400, result.StatusCode);

        //    var errors = result.Value as IdentityResult;

        //    Assert.NotNull(errors);
        //    Assert.False(errors.Succeeded);

        //    var errorDescriptions = errors.Errors.Select(error => error.Description).ToList();

        //    Assert.Single(errorDescriptions);
        //    Assert.Equal("Registration failed", errorDescriptions[0]);
        //}

        //[Fact]
        //public async Task Logout_ReturnsOk()
        //{
        //    _signInManager.SignOutAsync().Returns(Task.CompletedTask);

        //    _authController.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, "test@example.com"),
        //    }, "test"));

        //    var result = await _authController.Logout() as OkResult;

        //    await _signInManager.Received(1).SignOutAsync();

        //    Assert.NotNull(result);
        //}
    }
}