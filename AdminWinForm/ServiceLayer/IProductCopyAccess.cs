using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    internal interface IProductCopyAccess
    {
        Task<List<ProductCopy>> GetProductCopies();
        Task<int> AddProductCopy(string tokenToUse, ProductCopy productCopy);
       
        Task<bool> DeleteProductCopy(string serialNumber);
        Task<ProductCopy> GetProductCopyBySerialNumberAndProductId(string serialNumber, int productId);

        HttpStatusCode CurrentHttpStatusCode { get; set; }
    }
}
