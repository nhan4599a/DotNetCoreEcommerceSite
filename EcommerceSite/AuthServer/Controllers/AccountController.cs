using AuthServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Login Error", "Email or password is invalid");
                return View();
            }
            var user = await SignInManager.UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Login Error", "Email or password is in correct");
                return View();
            }
            var signInResult = await SignInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                return Redirect("/");
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("Login Error", "Account is locked out " + user.LockoutEnd);
            }
            else if (signInResult.IsNotAllowed)
            {
                ModelState.AddModelError("Login Error", "Email is not confirmed");
            }
            return View();
        }
    }
}
