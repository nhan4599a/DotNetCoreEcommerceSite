using EcommerceSite.Helper;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class SearchController : Controller
    {
        private const int PAGE_SIZE = 9;
        private readonly ApiCaller caller;

        public SearchController(ApiCaller caller)
        {
            this.caller = caller;
        }

        public async Task<IActionResult> SearchResult(SearchInputModel searchInput, int pageNumber = 1)
        {
            var categories = await caller.GetCategories();
            var products = await caller.SearchProducts(searchInput);
            if (searchInput.CategoryId != 0)
            {
                try
                {
                    ViewBag.CategoryName = categories.ToList()[searchInput.CategoryId - 1].Name;
                }
                catch (Exception) { }
            }
            ViewBag.Keyword = searchInput.Keyword;
            ViewBag.PageNumber = pageNumber;
            ViewBag.MaxPageNumber = (products.Count() / PAGE_SIZE) + 1;
            return View(new SearchResultPageModel
            {
                Categories = categories,
                Products = products.Paginate(pageNumber, PAGE_SIZE)
            });
        }
    }
}
