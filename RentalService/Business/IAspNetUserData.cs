using RentalService.Models;

namespace RentalService.Business
{
    public interface IAspNetUserData
    {
        string GetAspNetUserById(string id);
        string GetAspNetIdByUserName(string userName);
    }
}
