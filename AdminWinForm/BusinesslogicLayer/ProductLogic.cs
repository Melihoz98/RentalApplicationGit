using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdminWinForm.Models;
using AdminWinForm.Security;
using AdminWinForm.ServiceLayer; 

namespace AdminWinForm.BusinesslogicLayer
{
    public class ProductLogic
    {
        readonly IProductAccess _productAccess;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }

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
            int insertedProductId = -1;
            Product newProduct = new Product(productName, description, hourlyPrice, categoryID, imagePath);

            // Get token
            TokenState currentState = TokenState.Valid; // Presumed state
            string? tokenValue = await GetToken(currentState);
            if (tokenValue != null)
            {
                insertedProductId = await _productAccess.AddProduct(tokenValue, newProduct);

                if (_productAccess.CurrentHttpStatusCode == HttpStatusCode.Unauthorized)
                {
                    currentState = TokenState.Invalid;
                    insertedProductId = -2;
                }
            }
            else
            {
                currentState = TokenState.Invalid;
                tokenValue = await GetToken(currentState);

                if (tokenValue != null)
                {
                    insertedProductId = await _productAccess.AddProduct(tokenValue, newProduct);
                }
            }

            return insertedProductId;
        }





        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productAccess.DeleteProduct(productId);
        }


        private async Task<string?> GetToken(TokenState useState)
        {
            TokenManager tokenHelp = new TokenManager();
            string? foundToken = await tokenHelp.GetToken(useState);
            return foundToken;
        }
    }
}
