using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class ProductDtoConvert
    {
        // Convert from Product collection to ProductDto collection
        public static List<ProductDto> FromProductCollection(List<Product> products)
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                productDtos.Add(FromProduct(product));
            }
            return productDtos;
        }

        // Convert from Product to ProductDto
        public static ProductDto FromProduct(Product product)
        {
            return new ProductDto
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                HourlyPrice = (decimal)product.HourlyPrice,
                CategoryID = (int)product.CategoryID,
                ImagePath = product.ImagePath
            };
        }

        // Convert from ProductDto to Product
        public static Product ToProduct(ProductDto productDto)
        {
            return new Product
            {
                ProductID = productDto.ProductID,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                HourlyPrice = productDto.HourlyPrice,
                CategoryID = productDto.CategoryID,
                ImagePath = productDto.ImagePath
            };
        }
    }
}
