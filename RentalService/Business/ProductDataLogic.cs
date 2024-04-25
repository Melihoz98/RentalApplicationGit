﻿using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;
using RentalService.ModelConversion;
using System;
using System.Collections.Generic;

namespace RentalService.Business
{
    public class ProductDataLogic : IProductData
    {
        private readonly IProductAccess _productAccess;

        public ProductDataLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public ProductDto? GetById(int id)
        {
            try
            {
                Product product = _productAccess.GetProductById(id);
                return ProductDtoConvert.FromProduct(product);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting product by ID: {ex.Message}");
                return null;
            }
        }

        public List<ProductDto?>? GetAllProducts()
        {
            try
            {
                List<Product> products = _productAccess.GetProductAll();
                return ProductDtoConvert.FromProductCollection(products);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all products: {ex.Message}");
                return null;
            }
        }

        public int CreateProduct(ProductDto productToAdd)
        {
            try
            {
                Product product = ProductDtoConvert.ToProduct(productToAdd);
                return _productAccess.AddProduct(product);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product: {ex.Message}");
                return 0;
            }
        }

        public void UpdateProduct(ProductDto productToUpdate)
        {
            try
            {
                Product product = ProductDtoConvert.ToProduct(productToUpdate);
                _productAccess.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error updating product: {ex.Message}");
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _productAccess.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting product: {ex.Message}");
            }
        }
    }
}
