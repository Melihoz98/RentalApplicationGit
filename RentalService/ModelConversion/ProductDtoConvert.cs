using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class ProductDtoConvert
    {
        // Convert from Product collection to ProductDto collection
        public static List<ProductDto?>? FromProductCollection(List<Product> products)
        {
            List<ProductDto?>? productDtos = new List<ProductDto>();
            if(products != null)
            {
                productDtos = new List<ProductDto?>();
                
                foreach (var product in products)
            {
                    if(product != null)
                    {
                        ProductDto? dto = FromProduct(product);
                    productDtos.Add(dto);
                    }
               
            }
            }
            
            return productDtos;
        }

        // Convert from Product to ProductDto
        public static ProductDto? FromProduct(Product product)
        {
            ProductDto? productDto = null;
            if(product != null)
            {
                productDto = new ProductDto(product.ProductName,
                    product.Description,
                    product.HourlyPrice,
                    product.CategoryID,
                    product.ImagePath)
                { 
                    ProductID = product.ProductID
                };
                   
                  
            }
            return productDto;

           
        }

        // Convert from ProductDto to Product
        public static Product? ToProduct(ProductDto productDto)
        {
            Product? product = null;
            if(productDto != null)
            {
                product = new Product( productDto.ProductID,
                    productDto.ProductName,
                    productDto.Description,
                    productDto.HourlyPrice,
                    productDto.CategoryID,
                    productDto.ImagePath);

            }
            return product;
           
        }
    }
}
