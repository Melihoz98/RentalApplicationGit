using RentalService.DTO;

namespace RentalService.Business
{
    public interface IProductData
    {
        ProductDto? GetByID(int id);
        List<ProductDto?>? GetAllProducts();
        void AddProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
