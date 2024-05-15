using System;
using System.Threading.Tasks;
using AdminWinForm.ServiceLayer;
using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.BusinessLogicLayer
{
    public class AuthLogic
    {
        private readonly IAuthAccess _authAccess;

        public AuthLogic()
        {
            _authAccess = new AuthAccess();
        }

        public async Task<string?> Login(string userName, string password)
        {
            try
            {
                User user = new User { UserName = userName, PasswordHash = password };
                return await _authAccess.Login(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while logging in: {ex.Message}");
                return null;
            }
        }

        public async Task<int> Register(string userName, string password)
        {
            try
            {
                User user = new User { UserName = userName, PasswordHash = password };
                User registeredUser = await _authAccess.Register(user);
                // You may return some indication of successful registration, such as the user ID
                // For simplicity, I'm returning 0 here.
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while registering: {ex.Message}");
                return -1; // Return -1 to indicate registration failure
            }
        }
    }
}
