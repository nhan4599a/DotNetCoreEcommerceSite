using EcommerceSite.Helper;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiCaller caller;

        public HomeController(ApiCaller caller)
        {
            this.caller = caller;
        }

        public async Task<IActionResult> Home()
        {
            var categories = await caller.GetCategories();
            var hotProducts = await caller.GetHotProducts();
            var newProducts = await caller.GetNewProducts();
            HomePageModel model = new() { Categories = categories, HotProducts = hotProducts, NewProducts = newProducts };
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
