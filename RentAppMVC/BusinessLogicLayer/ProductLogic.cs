using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class ProductLogic
    {
        private readonly IProductAccess _productAccess;

        public ProductLogic()
        {
            _productAccess = new ProductAccess();
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
