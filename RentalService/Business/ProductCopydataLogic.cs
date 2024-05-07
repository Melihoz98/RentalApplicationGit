using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using RentalService.ModelConversion;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class ProductCopyDataLogic : IProductCopyData
    {
        private readonly IProductCopyAccess _productCopyAccess;

        public ProductCopyDataLogic(IProductCopyAccess productCopyAccess)
        {
            _productCopyAccess = productCopyAccess;
        }

        public ProductCopyDto? GetBySerialNumber(string serialNumber)
        {
            try
            {
                ProductCopy productCopy = _productCopyAccess.GetProductCopyBySerialNumber(serialNumber);
                return ProductCopyDtoConvert.FromProductCopy(productCopy);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting product copy by serial number: {ex.Message}");
                return null;
            }
        }

        public List<ProductCopyDto?>? GetAllProductCopies()
        {
            try
            {
                List<ProductCopy> productCopies = _productCopyAccess.GetProductCopyAll();
                return ProductCopyDtoConvert.FromProductCopyCollection(productCopies);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all product copies: {ex.Message}");
                return null;
            }
        }

        public void CreateProductCopy(ProductCopyDto productCopyToAdd)
        {
            try
            {
                ProductCopy productCopy = ProductCopyDtoConvert.ToProductCopy(productCopyToAdd);
                _productCopyAccess.AddProductCopy(productCopy);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product copy: {ex.Message}");
            }
        }

        public void UpdateProductCopy(ProductCopyDto productCopyToUpdate)
        {
            try
            {
                ProductCopy productCopy = ProductCopyDtoConvert.ToProductCopy(productCopyToUpdate);
                _productCopyAccess.UpdateProductCopy(productCopy);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error updating product copy: {ex.Message}");
            }
        }

        public void DeleteProductCopy(string serialNumber)
        {
            try
            {
                _productCopyAccess.DeleteProductCopy(serialNumber);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting product copy: {ex.Message}");
            }
        }

        public List<ProductCopyDto?>? GetAllProductCopiesByProductID(int productID)
        {
            try
            {
                List<ProductCopy> productCopies = _productCopyAccess.GetAllProductCopyByProductID(productID);
                return ProductCopyDtoConvert.FromProductCopyCollection(productCopies);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all product copies by product ID: {ex.Message}");
                return null;
            }
        }
    }
}