using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.Security;
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
            int insertedProductCopyId = -1;
            ProductCopy newProductCopy = new ProductCopy(serialNumber, productId);

            // Get token
            TokenState currentState = TokenState.Valid; // Presumed state
            string? tokenValue = await GetToken(currentState);
            if (tokenValue != null)
            {
                insertedProductCopyId = await _productCopyAccess.AddProductCopy(tokenValue, newProductCopy);

                if (_productCopyAccess.CurrentHttpStatusCode == HttpStatusCode.Unauthorized)
                {
                    currentState = TokenState.Invalid;
                    insertedProductCopyId = -2;
                }
            }
            else
            {
                currentState = TokenState.Invalid;
                tokenValue = await GetToken(currentState);

                if (tokenValue != null)
                {
                    insertedProductCopyId = await _productCopyAccess.AddProductCopy(tokenValue, newProductCopy);
                }
            }

            return insertedProductCopyId;
        }

        public async Task<bool> DeleteProductCopy(string serialNumber)
        {
            return await _productCopyAccess.DeleteProductCopy(serialNumber);
        }

        private async Task<string?> GetToken(TokenState useState)
        {
            TokenManager tokenHelp = new TokenManager();
            string? foundToken = await tokenHelp.GetToken(useState);
            return foundToken;
        }
    }
}
