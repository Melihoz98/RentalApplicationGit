using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public interface IProductAccess
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int productID);
        Task<int> AddProduct(string tokenToUse, Product product);
        
        Task<bool> DeleteProduct(int productID);

        HttpStatusCode CurrentHttpStatusCode { get; set; }
    }
}
