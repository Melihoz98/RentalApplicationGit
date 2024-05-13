using RentalService.Models;

namespace RentalService.DataAccess
{
    public interface IProductAccess
    {
        Product GetProductById(int id);
        List<Product> GetProductAll();
        int AddProduct(Product product);
       
        void DeleteProduct(int id);

    }
}
