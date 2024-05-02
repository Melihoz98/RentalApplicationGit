using Newtonsoft.Json;
using RentAppMVC.Models;
using System.Text;

namespace RentAppMVC.ServiceLayer
{
    public class AspNetUserAccess : IAspNetUserAccess
    {
        private readonly IServiceConnection _aspNetUserService;
        private readonly string _serviceBaseUrl = "https://localhost:7023/api/AspNetUser/";

        public AspNetUserAccess()
        {
            _aspNetUserService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<AspNetUser?> GetAspNetUserById(string id)
        {
            AspNetUser user = new AspNetUser();

            HttpResponseMessage? response = await _aspNetUserService.GetById(id);
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<AspNetUser>(jsonString);
            }

            return user;
        }

        public async Task<string?> GetAspNetIdByUserName(string userName)
        {
            string id = null;

            // Assuming your API endpoint supports querying by userName
            HttpResponseMessage? response = await _aspNetUserService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<AspNetUser>>(jsonString);
                var user = users.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    id = user.Id;
                }
            }

            return id;
        }
    }
}
