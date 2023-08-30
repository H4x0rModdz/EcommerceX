using EcommerceAPI.Models;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerResponse(200, "The list of users was successfully returned.", typeof(List<User>))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "The specified user was found and returned.", typeof(User))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        [SwaggerResponse(404, "The specified user was not found.")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "The specified user was successfully updated.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.", typeof(IEnumerable<IdentityError>))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserModel model)
        {
            var result = await _userService.UpdateAsync(id, model);
            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "The specified user was successfully deleted.")]
        [SwaggerResponse(400, "The operation failed. The response includes a list of errors describing the reason for the failure.", typeof(IEnumerable<IdentityError>))]
        [SwaggerResponse(401, "The specified user is not authorized to perform this operation.")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }
    }
}