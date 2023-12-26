using Microsoft.AspNetCore.Mvc;
using SIS_2_.Models;
using System.Net.NetworkInformation;
using SIS_2_.Data;
using SIS_2_.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace SIS_2_.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly StudentDataContext context;
        public AccountController(StudentDataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the interest already exists
                var existingInterest = context.Accounts.FirstOrDefault(i => i.Email == model.Email);
                if(existingInterest == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
                else if (model.Email == "admin@admin.com" && model.Password == "admin")
                {
                    var user = context.Accounts.FirstOrDefault(i => i.Email == model.Email);
                    var currentDate = DateTime.Now;
                    var utcTimestamp = currentDate.ToUniversalTime();

                    var log = new ActivityLog
                    {
                        UserId = user.AccountId.ToString(),
                        Timestamp = utcTimestamp,
                        ActionType = "Login", // Example action type
                    };
                    context.ActivityLogs.Add(log);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                   
                }
                else
                {
                    var user = context.Accounts.FirstOrDefault(i => i.Email == model.Email);
                    var currentDate = DateTime.Now;
                    var utcTimestamp = currentDate.ToUniversalTime();
                    var log = new ActivityLog
                    {
                        UserId = user.AccountId.ToString(),
                        Timestamp = utcTimestamp,
                        ActionType = "Login", // Example action type
                    };
                    context.ActivityLogs.Add(log);
                    context.SaveChanges();
                    return RedirectToAction("List2", "Student");
                    
                }
            }
             return View(model);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            // Your sign-in action logic goes here
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if an account with the same email already exists
                var existingAccount = context.Accounts.FirstOrDefault(i => i.Email == model.Email);

                if (existingAccount == null)
                {
                    // Create a new account
                    var account = new Account
                    {
                        Email = model.Email,
                        Password = model.Password
                        // Add other properties as needed
                    };

                    // Add the account to the context and save changes
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    var currentDate = DateTime.Now;
                    var utcTimestamp = currentDate.ToUniversalTime();
                    var user = context.Accounts.FirstOrDefault(i => i.Email == model.Email);
                    var log = new ActivityLog
                    {
                        UserId = user.AccountId.ToString(),
                        Timestamp = utcTimestamp,
                        ActionType = "SignUp",
                    };
                    context.ActivityLogs.Add(log);
                    context.SaveChanges();
                    // Redirect to the login page or another page as needed
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // An account with the same email already exists
                    ModelState.AddModelError(nameof(model.Email), "An account with this email already exists.");
                }
            }

            // If we reach here, there are validation errors, so return the view with the model
            return View(model);
        }

        public IActionResult Logout()
        {
            var currentDate = DateTime.Now;
            var utcTimestamp = currentDate.ToUniversalTime();
            var log = new ActivityLog
            {
                UserId = "user",
                Timestamp = utcTimestamp,
                ActionType = "LogOut", // Example action type
            };
            context.ActivityLogs.Add(log);
            context.SaveChanges();
            return RedirectToAction("Login", "Account");
        }
    }

}
