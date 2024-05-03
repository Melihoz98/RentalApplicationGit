using RentalService.DataAccess;

namespace RentalService.Business
{
    public class AspNetUserDataLogic : IAspNetUserData
    {
        private readonly IAspNetUserAccess _aspNetUserAccess;

        public AspNetUserDataLogic(IAspNetUserAccess aspNetUserAccess)
        {
            _aspNetUserAccess = aspNetUserAccess;
        }

        public string GetAspNetUserById(string id)
        {
            try
            {
                return _aspNetUserAccess.GetAspNetUserById(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"An error occurred while getting ASP.NET user by ID: {ex.Message}");
            }
        }

        public string GetAspNetIdByUserName(string userName)
        {
            try
            {
                return _aspNetUserAccess.GetAspNetIdByUserName(userName);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"An error occurred while getting ASP.NET user ID by user name: {ex.Message}");
            }
        }
    }
}
