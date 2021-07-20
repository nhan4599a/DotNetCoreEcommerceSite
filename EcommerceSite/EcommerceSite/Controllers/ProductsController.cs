using EcommerceSite.Helper;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> GetProducts(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                IEnumerable<ProductModel> productsOfCategory = await ApiCaller.getInstance().GetProductsOfCategory(categoryId.Value);
                return View("Product", productsOfCategory);
            }
            IEnumerable<ProductModel> products = await ApiCaller.getInstance().GetProducts();
            return View("Product", products);
        }
    }
}
