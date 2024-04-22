using RentalService.Models;
using RentalService.DTO;
using System;
using System.Collections.Generic;

namespace RentalService.ModelConversion
{
    public class ProductCopyDtoConvert
    {
        public static List<ProductCopyDto?>? FromProductCopyCollection(List<ProductCopy> inProductCopies)
        {
            List<ProductCopyDto?>? aProductCopyReadDtoList = null;
            if (inProductCopies != null)
            {
                aProductCopyReadDtoList = new List<ProductCopyDto?>();
                ProductCopyDto? tempDto;
                foreach (ProductCopy aProductCopy in inProductCopies)
                {
                    if (aProductCopy != null)
                    {
                        tempDto = FromProductCopy(aProductCopy);
                        aProductCopyReadDtoList.Add(tempDto);
                    }
                }
            }
            return aProductCopyReadDtoList;
        }

        public static ProductCopyDto? FromProductCopy(ProductCopy inProductCopy)
        {
            ProductCopyDto? aProductCopyDto = null;
            if (inProductCopy != null)
            {
                aProductCopyDto = new ProductCopyDto(inProductCopy.SerialNumber);
            }
            return aProductCopyDto;
        }
    }
}
