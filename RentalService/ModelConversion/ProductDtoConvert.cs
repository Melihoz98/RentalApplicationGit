using RentalService.Models;
using RentalService.DTO;


namespace RentalService.ModelConversion
{
    public class ProductDtoConvert
    {
        // Convert from Product collection to ProductDto collection
        public static List<ProductDto?>? FromProductCollection(List<Product> inProducts)
        {
            List<ProductDto?>? productReadDtoList = null;
            if (inProducts != null)
            {
                productReadDtoList = new List<ProductDto?>();
                foreach (Product aProduct in inProducts)
                {
                    if (aProduct != null)
                    {
                        ProductDto? dto = FromProduct(aProduct);
                        productReadDtoList.Add(dto);
                    }
                }
            }
            return productReadDtoList;
        }

        // Convert from Product to ProductDto
        public static ProductDto? FromProduct(Product inProduct)
        {
            ProductDto? aProductReadDto = null;
            if (inProduct != null)
            {
                aProductReadDto = new ProductDto(inProduct.ProductName, inProduct.Description, inProduct.HourlyPrice, inProduct.Inventory, inProduct.CategoryID);
            }
            return aProductReadDto;
        }

        // Convert from ProductDto to Product
        public static Product? ToProduct(ProductDto inDto)
        {
            Product? aProduct = null;
            if (inDto != null)
            {
                aProduct = new Product(inDto.ProductName, inDto.Description, inDto.HourlyPrice, inDto.Inventory, inDto.CategoryID);
            }
            return aProduct;
        }
    }
}
