using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

namespace RentAppMVC.BusinessLogicLayer
{
    public class ProductCopyLogic
    {
        private readonly IProductCopyAccess _productCopyAccess;

        public ProductCopyLogic()
        {
            _productCopyAccess = new ProductCopyAccess();
        }

        public async Task<ProductCopy?> GetProductCopyBySerialNumber(string serialNumber)
        {
            return await _productCopyAccess.GetBySerialNumber(serialNumber);
        }

        public async Task<List<ProductCopy>?> GetAllProductCopies()
        {
            return await _productCopyAccess.GetAllProductCopies();
        }


        public async Task<List<ProductCopy>?> GetAllProductCopyByID(int productID)  {
        
          
            return await _productCopyAccess.GetAllProductCopiesById(productID);
    }


}
}