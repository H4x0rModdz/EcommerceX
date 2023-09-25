using EcommerceAPI.Models;
using Newtonsoft.Json;

namespace EcommerceWeb.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7039/api/Users");
            response.EnsureSuccessStatusCode();
            var usersJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<User>>(usersJson);
        }
    }
}