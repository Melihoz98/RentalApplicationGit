using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWinForm.ServiceLayer
{
    internal interface IBusinessCustomerAccess
    {
        Task<List<BusinessCustomer>> GetBusinessCustomers();
        Task<BusinessCustomer> GetBusinessCustomerById(string customerId);
    }
}
