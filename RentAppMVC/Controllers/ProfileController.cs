using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;

public class ProfileController : Controller
{
    private readonly PrivateCustomerLogic _privateCustomerLogic;
    private readonly BusinessCustomerLogic _businessCustomerLogic;
    private readonly IAspNetUserAccess _aspNetUserAccess;

    public ProfileController(PrivateCustomerLogic privateCustomerLogic, BusinessCustomerLogic businessCustomerLogic, IAspNetUserAccess aspNetUserAccess)
    {
        _privateCustomerLogic = privateCustomerLogic;
        _businessCustomerLogic = businessCustomerLogic;
        _aspNetUserAccess = aspNetUserAccess;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveCustomer(ProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.CustomerType == "private")
            {
                PrivateCustomer privateCustomer = model.PrivateCustomer;
                string userName = User.Identity.Name;
                string userId = await _aspNetUserAccess.GetAspNetIdByUserName(userName);
                privateCustomer.CustomerID = userId;
                await _privateCustomerLogic.AddPrivateCustomer(privateCustomer);
            }
            else if (model.CustomerType == "business")
            {
                BusinessCustomer businessCustomer = model.BusinessCustomer;
                string userName = User.Identity.Name;
                string userId = await _aspNetUserAccess.GetAspNetIdByUserName(userName);
                businessCustomer.CustomerID = userId;
                await _businessCustomerLogic.AddBusinessCustomer(businessCustomer);
            }
            return RedirectToAction("Index");
        }
        // Handle invalid model state
        return View("Index", model);
    }


}
