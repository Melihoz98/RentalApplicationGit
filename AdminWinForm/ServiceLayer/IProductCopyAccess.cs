using RentalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    internal interface IProductCopyAccess
    {
        Task<List<ProductCopy>> GetProductCopies();
        Task<int> AddProductCopy(ProductCopy productCopy);
        Task<bool> UpdateProductCopy(ProductCopy productCopy);
        Task<bool> DeleteProductCopy(string serialNumber);
        Task<ProductCopy> GetProductCopyBySerialNumberAndProductId(string serialNumber, int productId);
    }
}
