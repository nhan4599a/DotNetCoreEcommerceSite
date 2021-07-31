using EcommerceSite.Helper;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiCaller caller;

        public ProductsController(ApiCaller caller)
        {
            this.caller = caller;
        }

        public async Task<IActionResult> GetProducts(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                IEnumerable<ProductModel> productsOfCategory = await ApiCaller.GetInstance().GetProductsOfCategory(categoryId.Value);
                return View("Product", productsOfCategory);
            }
            IEnumerable<ProductModel> products = await caller.GetProducts();
            return View("Product", products);
        }

        public static string GetImageUrl(string productId)
        {
            return ApiCaller.GetImageUrl(productId);
        }

        public async Task<IActionResult> Detail(string productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }
            var categories = await caller.GetCategories();
            var product = await caller.GetProductDetail(productId);
            if (product == null)
                return NotFound();
            return View(new ProductDetailPageModel { Categories = categories, Product = product });
        }
    }
}
