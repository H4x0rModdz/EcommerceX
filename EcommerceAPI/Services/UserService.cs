using EcommerceAPI.Models;
using EcommerceAPI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IdentityResult> UpdateAsync(string id, UpdateUserModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);

            user.FullName = model.FullName;
            user.Address = model.Address;
            user.BirthDate = model.BirthDate;
            user.IsSeller = false;
            user.Balance = 0;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return await _userRepository.DeleteAsync(user);
        }
    }
}