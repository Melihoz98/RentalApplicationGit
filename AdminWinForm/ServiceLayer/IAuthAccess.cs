using System.Threading.Tasks;
using AdminWinForm.Models;

namespace AdminWinForm.ServiceLayer
{
    public interface IAuthAccess
    {
        Task<string> Login(User user);
        Task<User> Register(User user);
    }
}
