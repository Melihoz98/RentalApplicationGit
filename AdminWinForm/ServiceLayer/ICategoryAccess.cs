using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    public interface ICategoryAccess
    {
        Task<List<Category>> GetCategories(string tokenToUse);
        Task<Category> GetCategoryById(int categoryId);
        Task<int> AddCategory(Category category);
       
        Task<bool> DeleteCategory(int categoryId);
        HttpStatusCode CurrentHttpStatusCode { get; set; }
    }
}
