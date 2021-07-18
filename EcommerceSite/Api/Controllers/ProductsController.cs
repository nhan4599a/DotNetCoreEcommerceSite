using Api.Helper;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AssignmentContext db;

        public ProductsController()
        {
            db = new AssignmentContext();
        }

        [HttpGet]
        [Route("all")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> GetAll()
        {
            return new ApiJsonResponseModel<IEnumerable<ProductModel>>
            {
                Data = db.Products.MapToProductModel()
            };
        }

        [HttpGet]
        [Route("get")]
        public ApiJsonResponseModel<ProductModel> Get([FromQuery] int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
                return new ApiJsonResponseModel<ProductModel>
                {
                    ResponseCode = 404,
                    ErrorMessage = $"Product with id=${id} not found",
                    Data = null
                };
            return new ApiJsonResponseModel<ProductModel>
            {
                Data = product.MapToProductModel()
            };
        }

        [HttpGet]
        [Route("find")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> Find(int categoryId)
        {
            return new ApiJsonResponseModel<IEnumerable<ProductModel>>
            {
                Data = db.Products.Where(item => item.CategoryId == categoryId).MapToProductModel()
            };
        }

        [HttpPost]
        [Route("add")]
        public ApiJsonResponseModel<ProductModel> AddProduct([FromBody] ProductModel productModel, [FromQuery] int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category == null)
                return new ApiJsonResponseModel<ProductModel> { ErrorMessage = "category is not existed", ResponseCode = 404, Data = null };
            if (!ModelState.IsValid)
                return new ApiJsonResponseModel<ProductModel> { ErrorMessage = "model isn't met condition", ResponseCode = 400, Data = null };
            var product = new Product
            {
                Name = productModel.Name,
                Decription = productModel.Description,
                Price = productModel.Price,
                CategoryId = categoryId
            };
            db.Products.Add(product);
            db.SaveChanges();
            return new ApiJsonResponseModel<ProductModel> { Data = product.MapToProductModel() };
        }
    }
}
