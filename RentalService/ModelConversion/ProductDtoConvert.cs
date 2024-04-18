using RentalService.Models;
using RentalService.DTO;


namespace RentalService.ModelConversion
{
    public class ProductDtoConvert
    {
        // Convert from Product collection to ProductDto collection
        public static List<ProductDto?>? FromProductCollection(List<Product> inProducts)
        {
            List<ProductDto?>? productDtoList = null;
            if (inProducts != null)
            {
                productDtoList = new List<ProductDto?>();
                foreach (Product product in inProducts)
                {
                    if (product != null)
                    {
                        ProductDto? dto = FromProduct(product);
                        productDtoList.Add(dto);
                    }
                }
            }
            return productDtoList;
        }

        // Convert from Product to ProductDto
        public static ProductDto? FromProduct(Product product)
        {
            ProductDto? productDto = null;
            if (product != null)
            {
                productDto = new ProductDto(product.ProductName, product.Description, product.HourlyPrice, product.Inventory, product.CategoryID);
            }
            return productDto;
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
