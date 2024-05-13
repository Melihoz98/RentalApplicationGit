﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalService.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.BusinesslogicLayer
{
    public class ProductCopyLogic
    {
        private readonly IProductCopyAccess _productCopyAccess;

        public ProductCopyLogic()
        {
            _productCopyAccess = new ProductCopyServiceAccess();
        }

        public async Task<List<ProductCopy>?> GetAllProductCopies()
        {
            List<ProductCopy>? foundProductCopies = null;
            if (_productCopyAccess != null)
            {
                foundProductCopies = await _productCopyAccess.GetProductCopies();
            }
            return foundProductCopies;
        }

        public async Task<int> AddProductCopy(string serialNumber, int productId)
        {
            ProductCopy newProductCopy = new ProductCopy(serialNumber, productId);
            int insertedProductCopyId = await _productCopyAccess.AddProductCopy(newProductCopy);
            return insertedProductCopyId;
        }

      


        public async Task<bool> DeleteProductCopy(string serialNumber)
        {
            // Attempt to delete the product copy using serial number and product ID
            return await _productCopyAccess.DeleteProductCopy(serialNumber);
        }

    }
}
