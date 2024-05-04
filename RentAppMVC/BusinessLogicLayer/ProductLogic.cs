using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class ProductLogic
    {
        private readonly IProductAccess _productAccess;

        public ProductLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productAccess.GetAllProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productAccess.GetProductById(productId);
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _productAccess.GetProductsByCategoryId(categoryId);
        }
    }
}
