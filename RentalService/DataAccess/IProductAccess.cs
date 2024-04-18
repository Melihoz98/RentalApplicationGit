using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IProductAccess
    {
        Product GetProductById(int id);
        List<Product> GetProductAll();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);

    }
}
