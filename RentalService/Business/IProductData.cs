using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.Business
{
    public interface IProductData
    {
        ProductDto? GetById(int id);
        List<ProductDto?>? GetAllProducts();
        int CreateProduct(ProductDto productToAdd);
       
        void DeleteProduct(int id);
    }
}
