using RentalService.Models;
using RentalService.DTO;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class ProductCopyDtoConvert
    {
        // Convert from ProductCopy collection to ProductCopyDto collection
        public static List<ProductCopyDto> FromProductCopyCollection(List<ProductCopy> productCopies)
        {
            List<ProductCopyDto> productCopyDtos = new List<ProductCopyDto>();
            foreach (var productCopy in productCopies)
            {
                productCopyDtos.Add(FromProductCopy(productCopy));
            }
            return productCopyDtos;
        }

        // Convert from ProductCopy to ProductCopyDto
        public static ProductCopyDto FromProductCopy(ProductCopy productCopy)
        {
            return new ProductCopyDto
            {
                ProductID = productCopy.ProductID,
                SerialNumber = productCopy.SerialNumber
            };
        }

        // Convert from ProductCopyDto to ProductCopy
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
