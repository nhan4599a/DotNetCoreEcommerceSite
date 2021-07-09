using AuthServer.Model;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AuthServer.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<IdentityUser> SignInManager { get; }

        public SmtpClient SmtpClient { get; }

        public IConfiguration Configuration { get; }

        public IIdentityServerInteractionService Interaction { get; }

        public AccountController(SignInManager<IdentityUser> signInManager, SmtpClient smtpClient, IConfiguration configuration, IIdentityServerInteractionService interaction)
        {
            SignInManager = signInManager;
            SmtpClient = smtpClient;
            Configuration = configuration;
            Interaction = interaction;
        }

        public IActionResult SignIn(string ReturnUrl = "~/")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginInputModel model)
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
                return Redirect(model.ReturnUrl);
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

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(LoginInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("SignUp Error", "Invalid input");
                return View();
            }
            IdentityUser user = new()
            {
                Email = model.Email,
                UserName = model.Email,
                NormalizedEmail = model.Email.ToUpper()
            };
            var identityResult = await SignInManager.UserManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                await SendUserValidationEmail(user);
                return RedirectToAction("SignIn");
            }
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("SignUp Error", error.Description);
            }
            return View("SignUp");
        }

        private async Task SendUserValidationEmail(IdentityUser user)
        {
            string baseUrl = Configuration["BaseUrl"];
            string token = await SignInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var subject = "Confirmation email";
            var htmlContent = string.Format("<p>Hello {0}, click on <a href=\"{1}/Account/ConfirmEmail?username={2}&token={3}\">here</a> to confirm your email</p>",
                user.UserName, baseUrl, HttpUtility.UrlEncode(user.UserName), HttpUtility.UrlEncode(token));
            var mailMessage = new MailMessage
            {
                From = new MailAddress("nhan0385790927@gmail.com"),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };
            mailMessage.To.Add(user.Email);
            SmtpClient.SendAsync(mailMessage, null);
        }

        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            IdentityUser user = await SignInManager.UserManager.FindByEmailAsync(username);
            _ = await SignInManager.UserManager.ConfirmEmailAsync(user, token);
            return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> SignOut(string logoutId)
        {
            await HttpContext.SignOutAsync();
            var logout = await Interaction.GetLogoutContextAsync(logoutId);
            return Redirect(string.IsNullOrEmpty(logout.PostLogoutRedirectUri) ? "~/" : logout.PostLogoutRedirectUri);
        }
    }
}
