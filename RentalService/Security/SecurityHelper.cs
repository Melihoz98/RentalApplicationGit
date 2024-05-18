using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RentalService.Security
{
    public class SecurityHelper
    {

        private readonly IConfiguration _configuration;

        public SecurityHelper(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }
        public SymmetricSecurityKey? GetSecurityKey()
        {
            SymmetricSecurityKey? SIGNING_KEY = null;
            string? SECRET_KEY = _configuration["SECRET_KEY"];
            if (!string.IsNullOrEmpty(SECRET_KEY))
            {
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }
            return SIGNING_KEY;
        }

        public bool IsValidUsernameAndPassword(string username, string password)
        {
            bool credentialsOk = false;
            string? allowedUsername = _configuration["AllowDesktopApp:Username"];
            string? allowedPassword = _configuration["AllowDesktopApp:Password"];
            if (allowedUsername != null && allowedPassword != null)
            {
                credentialsOk = (username.Equals(allowedUsername)) && (password.Equals(allowedPassword));
            }
            return credentialsOk;
        }
        public RoleEnum GetRoleEnum(string roleStr)
        {
            RoleEnum foundRoleEnum;
            bool wasSuccessful = Enum.TryParse(roleStr, out foundRoleEnum);
            if (!wasSuccessful)
            {
                foundRoleEnum = RoleEnum.User;
            }
            return foundRoleEnum;
        }


    }
}