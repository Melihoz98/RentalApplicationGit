using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IAspNetUserAccess
    {
        Task<AspNetUser?> GetAspNetUserById(string id);
        Task<string?> GetAspNetIdByUserName(string userName);
    }
}
