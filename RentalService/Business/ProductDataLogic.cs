using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;


namespace RentalService.Business
{
    public class ProductDataLogic : IProductData
    {
        private readonly IProductAccess _productAccess;

        public ProductDataLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public ProductDto? GetByID(int idToMatch)
        {
            ProductDto? foundProductDto;
            try
            {
                Product? foundProduct = _productAccess.GetProductById(idToMatch);
                foundProductDto = ModelConversion.ProductDtoConvert.FromProduct(foundProduct);
            }
            catch
            {
                foundProductDto = null;
            }
            return foundProductDto;
        }

        public List<ProductDto?>? GetAllProducts()
        {
            List<ProductDto?>? foundDtos;
            try
            {
                List<Product>? foundProducts = _productAccess.GetProductAll();
                foundDtos = ModelConversion.ProductDtoConvert.FromProductCollection(foundProducts);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string errorMessage = ex.Message;
                // Handle exception
            }
            return foundDtos;
        }
    }
}
