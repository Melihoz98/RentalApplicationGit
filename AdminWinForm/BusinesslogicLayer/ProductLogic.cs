﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWinForm.Models; 
using AdminWinForm.ServiceLayer; 

namespace AdminWinForm.BusinesslogicLayer
{
    public class ProductLogic
    {
        readonly IProductAccess _productAccess;

        public ProductLogic()
        {
            _productAccess = new ProductServiceAccess();
        }

        public async Task<List<Product>?> GetAllProducts()
        {
            List<Product>? foundProducts = null;
            if (_productAccess != null)
            {
                foundProducts = await _productAccess.GetProducts();
            }
            return foundProducts;
        }

        public async Task<int> AddProduct(string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            Product newProduct = new Product(productName, description, hourlyPrice, categoryID, imagePath);
            int insertedProductId = await _productAccess.AddProduct(newProduct);
            return insertedProductId;
        }

        public async Task<bool> UpdateProduct(int productId, string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            Product existingProduct = await _productAccess.GetProductById(productId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = productName;
                existingProduct.Description = description;
                existingProduct.HourlyPrice = hourlyPrice;
                existingProduct.CategoryID = categoryID;
                existingProduct.ImagePath = imagePath;

                return await _productAccess.UpdateProduct(existingProduct);
            }

            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productAccess.DeleteProduct(productId);
        }
    }
}