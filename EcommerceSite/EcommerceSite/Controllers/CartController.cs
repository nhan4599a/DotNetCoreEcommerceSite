using EcommerceSite.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceSite.Controllers
{
    public class CartController : Controller
    {
        public class CartItem
        {
            public string ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class CartOutputModel
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ImageUrl { get { return ProductsController.GetImageUrl(ProductId); } }
            public double Price { get; set; }
            public int Quantity { get; set; }
        }

        private Dictionary<string, CartOutputModel> cart;

        private ISession session;

        private ApiCaller caller;

        public CartController(IHttpContextAccessor httpContextAccessor, ApiCaller caller)
        {
            session = httpContextAccessor.HttpContext.Session;
            cart = JsonConvert.DeserializeObject<Dictionary<string, CartOutputModel>>(session.GetString("cart") ?? "{}");
            this.caller = caller;
        }

        public string Index()
        {
            return JsonConvert.SerializeObject(cart.Values);
        }

        [HttpPost]
        public async Task<JsonResult> Add(string productId)
        {
            var productModel = await caller.GetProductDetail(productId);
            if (string.IsNullOrEmpty(productId) || productModel == null)
                return Json(new ApiJsonResponseModel<string> { ResponseCode = 404, ErrorMessage = "Product not found", Data = null });
            try
            {
                cart.Add(productId, new CartOutputModel { ProductId = productId, ProductName = productModel.Name, Price = productModel.Price, Quantity = 1 });
                UpdateCartSession();
                return Json(new ApiJsonResponseModel<string> { Data = "Added new products to your cart successfully" });
            }
            catch
            {
                return Json(new ApiJsonResponseModel<string> { ResponseCode = 400, ErrorMessage = "Product already existed in cart" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update(IList<CartItem> list)
        {
            foreach (var item in list)
            {
                var id = item.ProductId;
                var quantity = item.Quantity;
                var product = await caller.GetProductDetail(id);
                if (product == null)
                    return Json(new ApiJsonResponseModel<string> { ResponseCode = 404, ErrorMessage = "Product is not found" });
                cart[id] = new CartOutputModel { ProductId = id, ProductName = product.Name, Price = product.Price, Quantity = quantity };
            }
            UpdateCartSession();
            return Json(new ApiJsonResponseModel<string> { Data = "Cart updated successfully" });
        }

        [HttpPost]
        public JsonResult Delete(string productId)
        {
            cart.Remove(productId);
            UpdateCartSession();
            return Json(new ApiJsonResponseModel<string> { Data = "Product removed successfully" });
        }

        private void UpdateCartSession()
        {
            session.SetString("cart", JsonConvert.SerializeObject(cart));
        }
    }
}
