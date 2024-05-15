using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminWinForm.Models;

namespace AdminWinForm.ServiceLayer
{
    public class AuthAccess : IAuthAccess
    {
        private readonly IServiceConnection _authService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/Auth/";

        public AuthAccess()
        {
            _authService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<string> Login(User user)
        {
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var serviceResponse = await _authService.CallServicePost(httpContent);

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    return await serviceResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Login failed. Please check your credentials.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while logging in: {ex.Message}");
            }
        }

        public async Task<User> Register(User user)
        {
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var serviceResponse = await _authService.CallServicePost(httpContent);

                if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                {
                    string responseData = await serviceResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(responseData);
                }
                else
                {
                    throw new Exception("Registration failed. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while registering: {ex.Message}");
            }
        }
    }
}
