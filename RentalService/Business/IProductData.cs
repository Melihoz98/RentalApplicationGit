using RentalService.DTO;

namespace RentalService.Business
{
    public interface IProductData
    {
        ProductDto? GetByID(int id);
        List<ProductDto?>? GetAllProducts();
    }
}
