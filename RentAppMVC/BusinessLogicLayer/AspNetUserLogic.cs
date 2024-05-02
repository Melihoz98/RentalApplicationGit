using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using System.Threading.Tasks;

namespace RentAppMVC.BusinessLogicLayer
{
    public class AspNetUserLogic
    {
        private readonly IAspNetUserAccess _aspNetUserAccess;

        public AspNetUserLogic(IAspNetUserAccess aspNetUserAccess)
        {
            _aspNetUserAccess = aspNetUserAccess;
        }

        public async Task<string?> GetAspNetIdByUserName(string userName)
        {
            return await _aspNetUserAccess.GetAspNetIdByUserName(userName);
        }
    }
}
