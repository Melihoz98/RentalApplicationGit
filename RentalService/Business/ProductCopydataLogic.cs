using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using System.Collections.Generic;
using System;
namespace RentalService.Business
{
    public class ProductCopydataLogic : IProductCopyData
    {
        private readonly IProductCopyAccess _productCopyAccess;

        public ProductCopydataLogic(IProductCopyAccess inProductCopyAccess)
        {
            _productCopyAccess = inProductCopyAccess;

        }

        public ProductCopyDto? GetBySerialNumber(string SerialNumberToMatch)
        {
            ProductCopyDto? foundProductCopyDto;
            try
            {
                ProductCopy? foundProductCopy = _productCopyAccess.GetBySerialNumber(SerialNumberToMatch);
                foundProductCopyDto = ModelConversion.ProductCopyDtoConvert.FromProductCopy(foundProductCopy);
            }
            catch
            {
                foundProductCopyDto = null;
            }
            return foundProductCopyDto;
        }

        public List<ProductCopyDto?>? GetProductCopiesAll()
        {
            List<ProductCopyDto?>? foundDtos;
            try
            {
                List<ProductCopy>? foundProductCopies = _productCopyAccess.GetProductCopiesAll();
                foundDtos = ModelConversion.ProductCopyDtoConvert.FromProductCopyCollection(foundProductCopies);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string xx = ex.Message;
            }
            return foundDtos;

        }

      
    }
}
