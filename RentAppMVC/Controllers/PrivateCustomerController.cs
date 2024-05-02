using Microsoft.AspNetCore.Mvc;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;
using RentAppMVC.ServiceLayer;
using System.Threading.Tasks;

public class PrivateCustomerController : Controller
{
    private readonly PrivateCustomerLogic _privateCustomerLogic;
    private readonly IAspNetUserAccess _aspNetUserAccess;

    public PrivateCustomerController(PrivateCustomerLogic privateCustomerLogic, IAspNetUserAccess aspNetUserAccess)
    {
        _privateCustomerLogic = privateCustomerLogic;
        _aspNetUserAccess = aspNetUserAccess;
    }

    // GET: PrivateCustomer/Index
    public IActionResult Index()
    {
        return View();
    }

    // POST: PrivateCustomer/Save
    [HttpPost]
    public async Task<IActionResult> Save(ProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            PrivateCustomer privateCustomer = model.PrivateCustomer;
            string userName = User.Identity.Name;
            string userId = await _aspNetUserAccess.GetAspNetIdByUserName(userName);
            privateCustomer.CustomerID = userId;
            await _privateCustomerLogic.AddPrivateCustomer(privateCustomer);

            return RedirectToAction("Index");
        }
        // Handle invalid model state
        return View("Index", model);
    }
}