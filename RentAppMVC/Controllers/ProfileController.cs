using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;

namespace RentAppMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly PrivateCustomerLogic _privateCustomerLogic;
        private readonly BusinessCustomerLogic _businessCustomerLogic;

        public ProfileController(PrivateCustomerLogic privateCustomerLogic, BusinessCustomerLogic businessCustomerLogic)
        {
            _privateCustomerLogic = privateCustomerLogic;
            _businessCustomerLogic = businessCustomerLogic;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SavePrivateCustomer(PrivateCustomer model)
        {
            if (ModelState.IsValid)
            {
                await _privateCustomerLogic.AddPrivateCustomer(model);
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBusinessCustomer(BusinessCustomer model)
        {
            if (ModelState.IsValid)
            {
                await _businessCustomerLogic.AddBusinessCustomer(model);
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}
