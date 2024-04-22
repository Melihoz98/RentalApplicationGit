using RentalService.DTO;

namespace RentalService.Business
{
    public interface IProductData
    {
        ProductDto? Get(int id);
        List<ProductDto?>? Get();
        int Add(ProductDto productToAdd);
        void Put(ProductDto productToUpdate);
        void DeleteProduct(int id);
    }
}
