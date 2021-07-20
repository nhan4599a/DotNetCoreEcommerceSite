using EcommerceSite.Helper;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Home()
        {
            ApiCaller apiCaller = ApiCaller.getInstance();
            var categories = await apiCaller.GetCategories();
            var hotProducts = await apiCaller.GetHotProducts();
            var newProducts = await apiCaller.GetNewProducts();
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
