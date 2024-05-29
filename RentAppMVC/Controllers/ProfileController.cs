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
        public async Task<IActionResult> SavePrivateCustomer(PrivateCustomer model)
        {
            if(ModelState.IsValid)
            {
                await _privateCustomerLogic.AddPrivateCustomer(model);
                return RedirectToAction("Index");
            }
            return View("index", model);
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
