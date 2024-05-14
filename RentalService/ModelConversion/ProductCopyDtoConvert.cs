using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class ProductCopyDtoConvert
    {
       
        public static List<ProductCopyDto> FromProductCopyCollection(List<ProductCopy> productCopies)
        {
            List<ProductCopyDto> productCopyDtos = new List<ProductCopyDto>();
            foreach (var productCopy in productCopies)
            {
                productCopyDtos.Add(FromProductCopy(productCopy));
            }
            return productCopyDtos;
        }

        
        public static ProductCopyDto FromProductCopy(ProductCopy productCopy)
        {
            return new ProductCopyDto
            {
                ProductID = productCopy.ProductID,
                SerialNumber = productCopy.SerialNumber
            };
        }

        
        public static ProductCopy ToProductCopy(ProductCopyDto productCopyDto)
        {
            return new ProductCopy
            {
                ProductID = productCopyDto.ProductID,
                SerialNumber = productCopyDto.SerialNumber
            };
        }
    }
}
