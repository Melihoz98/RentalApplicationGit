using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IAspNetUserAccess
    {
        string GetAspNetUserById(string id);
        string GetAspNetIdByUserName(string userName);
    }
}
