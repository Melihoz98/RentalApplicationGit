using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class BusinessCustomerController : Controller
    {
        private readonly BusinessCustomerLogic _businessCustomerLogic;
        private readonly IAspNetUserAccess _aspNetUserAccess;

        public BusinessCustomerController(BusinessCustomerLogic businessCustomerLogic, IAspNetUserAccess aspNetUserAccess)
        {
            _businessCustomerLogic = businessCustomerLogic;
            _aspNetUserAccess = aspNetUserAccess;
        }

        // GET: BusinessCustomer/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: BusinessCustomer/Save
        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessCustomer businessCustomer = model.BusinessCustomer;
                string userName = User.Identity.Name;
                string userId = await _aspNetUserAccess.GetAspNetIdByUserName(userName);
                businessCustomer.CustomerID = userId;
                await _businessCustomerLogic.AddBusinessCustomer(businessCustomer);

                return RedirectToAction("Index");
            }
            // Handle invalid model state
            return View("Index", model);
        }
    }
}
