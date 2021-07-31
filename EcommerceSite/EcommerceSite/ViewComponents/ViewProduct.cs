using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System.Threading.Tasks;

namespace EcommerceSite.ViewComponents
{
    [ViewComponent]
    public class ViewProduct : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductModel model)
        {
            return View("index", model);
        }
    }
}
