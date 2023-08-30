using EcommerceAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(string id);

        Task<IdentityResult> UpdateAsync(string id, UpdateUserModel model);

        Task<IdentityResult> DeleteAsync(string id);
    }
}