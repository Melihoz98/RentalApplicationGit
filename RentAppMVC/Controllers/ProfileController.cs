using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using System.Threading.Tasks;

namespace RentAppMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly PrivateCustomerLogic _privateCustomerLogic;
        private readonly BusinessCustomerLogic _businessCustomerLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileController(PrivateCustomerLogic privateCustomerLogic, BusinessCustomerLogic businessCustomerLogic, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _privateCustomerLogic = privateCustomerLogic;
            _businessCustomerLogic = businessCustomerLogic;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var privateCustomer = await _privateCustomerLogic.GetPrivateCustomerById(userId);
            var businessCustomer = await _businessCustomerLogic.GetBusinessCustomerById(userId);

            var model = new ProfileViewModel
            {
                PrivateCustomer = privateCustomer,
                BusinessCustomer = businessCustomer
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePrivateCustomer(ProfileViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var exists = await _privateCustomerLogic.CustomerExists(userId);
                if (exists)
                {
                    ModelState.AddModelError(string.Empty, "Customer with this ID already exists as either Private or Business.");
                    return View("Index", model);
                }

                var existingPrivateCustomer = await _privateCustomerLogic.GetPrivateCustomerById(userId);
                if (existingPrivateCustomer == null)
                {
                    model.PrivateCustomer.CustomerID = userId;
                    await _privateCustomerLogic.AddPrivateCustomer(model.PrivateCustomer);
                }
                else
                {
                    model.PrivateCustomer.CustomerID = userId;
                    await _privateCustomerLogic.UpdatePrivateCustomer(model.PrivateCustomer);
                }
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBusinessCustomer(ProfileViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var exists = await _privateCustomerLogic.CustomerExists(userId);
                if (exists)
                {
                    ModelState.AddModelError(string.Empty, "Customer with this ID already exists as either Private or Business.");
                    return View("Index", model);
                }

                var existingBusinessCustomer = await _businessCustomerLogic.GetBusinessCustomerById(userId);
                if (existingBusinessCustomer == null)
                {
                    model.BusinessCustomer.CustomerID = userId;
                    await _businessCustomerLogic.AddBusinessCustomer(model.BusinessCustomer);
                }
                else
                {
                    model.BusinessCustomer.CustomerID = userId;
                    await _businessCustomerLogic.UpdateBusinessCustomer(model.BusinessCustomer);
                }
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}
