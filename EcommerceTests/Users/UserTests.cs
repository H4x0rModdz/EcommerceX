using EcommerceAPI.Controllers;
using EcommerceAPI.Models;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTests.Users
{
    public class UserTests
    {
        [Fact]
        public async Task GetAll_ReturnsListOfUsers()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<User>());

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsType<List<User>>(okResult.Value);
            Assert.Empty(users);
        }

        [Fact]
        public async Task GetById_ReturnsUserById()
        {
            var userId = "1";
            var user = new User { Id = userId, UserName = "testuser" };

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync(user);

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.GetById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userId, returnedUser.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundForInvalidUserId()
        {
            var userId = "";

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.GetById(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContentOnSuccess()
        {
            var userId = "1";
            var updateUserModel = new UpdateUserModel { FullName = "New User" };

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.UpdateAsync(userId, updateUserModel))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.Update(userId, updateUserModel);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequestOnFailure()
        {
            var userId = "1";
            var updateUserModel = new UpdateUserModel { FullName = "New User" };
            var errors = new List<IdentityError> { new IdentityError { Description = "Error 1" } };

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.UpdateAsync(userId, updateUserModel))
                .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.Update(userId, updateUserModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnedErrors = Assert.IsType<List<IdentityError>>(badRequestResult.Value);
            Assert.Equal(errors, returnedErrors);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentOnSuccess()
        {
            var userId = "1";

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.DeleteAsync(userId))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.Delete(userId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequestOnFailure()
        {
            var userId = "1";
            var errors = new List<IdentityError> { new IdentityError { Description = "Error 1" } };

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.DeleteAsync(userId))
                .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

            var controller = new UsersController(userServiceMock.Object);

            var result = await controller.Delete(userId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnedErrors = Assert.IsType<List<IdentityError>>(badRequestResult.Value);
            Assert.Equal(errors, returnedErrors);
        }
    }
}