using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;

namespace RentAppMVC.ServiceLayer
{
    public interface IProductAccess
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
    }
}
