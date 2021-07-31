using Api.Helper;
using Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            db = dbContext;
            hostingEnvironment = environment;
        }

        [HttpGet]
        [Route("all")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> GetAll()
        {
            return new ApiJsonResponseModel<IEnumerable<ProductModel>>
            {
                Data = db.Products.AsNoTracking().Include(prod => prod.Category).MapToProductModel()
            };
        }

        [HttpGet]
        [Route("get")]
        public ApiJsonResponseModel<ProductModel> Get([FromQuery] string id)
        {
            Product product = db.Products.Include(prod => prod.Category).SingleOrDefault(prod => prod.Id.ToString() == id);
            if (product == null)
                return new ApiJsonResponseModel<ProductModel>
                {
                    ResponseCode = HttpResponseCode.NOT_FOUND,
                    ErrorMessage = ErrorMessages.PRODUCT_NOT_FOUND,
                    Data = null
                };
            return new ApiJsonResponseModel<ProductModel>
            {
                Data = product.MapToProductModel()
            };
        }

        [HttpGet]
        [Route("find")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> Find([FromQuery] int categoryId)
        {
            return new ApiJsonResponseModel<IEnumerable<ProductModel>>
            {
                Data = db.Products.AsNoTracking().Where(item => item.CategoryId == categoryId).MapToProductModel()
            };
        }

        [HttpPost]
        [Route("add")]
        public ApiJsonResponseModel<ProductModel> AddProduct([FromForm] ProductInputModel productModel, [FromQuery] int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category == null)
                return new ApiJsonResponseModel<ProductModel> { ErrorMessage = ErrorMessages.CATEGORY_NOT_FOUND, ResponseCode = HttpResponseCode.NOT_FOUND, Data = null };
            if (!ModelState.IsValid || Request.Form.Files.Count == 0)
                return new ApiJsonResponseModel<ProductModel> { ErrorMessage = ErrorMessages.MODEL_INVALID, ResponseCode = HttpResponseCode.BAD_REQUEST, Data = null };
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                CategoryId = categoryId
            };
            db.Products.Add(product);
            db.SaveChanges();
            SaveImage(Request.Form.Files[0], product.Id.ToString());
            return new ApiJsonResponseModel<ProductModel> { Data = product.MapToProductModel() };
        }

        [HttpPatch]
        [Route("edit")]
        public ApiJsonResponseModel<ProductModel> EditProduct([FromBody] ProductInputModel model, [FromQuery] int productId, [FromQuery] int categoryId)
        {
            if (!ModelState.IsValid)
                return new ApiJsonResponseModel<ProductModel> { Data = null, ErrorMessage = "Model is invalid", ResponseCode = HttpResponseCode.BAD_REQUEST };
            var product = db.Products.Find(productId);
            if (product == null)
                return new ApiJsonResponseModel<ProductModel> { Data = null, ErrorMessage = ErrorMessages.PRODUCT_NOT_FOUND, ResponseCode = HttpResponseCode.NOT_FOUND };
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategoryId = categoryId;
            try
            {
                return new ApiJsonResponseModel<ProductModel> { Data = product.MapToProductModel() };
            } catch (Exception)
            {
                return new ApiJsonResponseModel<ProductModel> { Data = null, ErrorMessage = ErrorMessages.CATEGORY_NOT_FOUND, ResponseCode = HttpResponseCode.NOT_FOUND };
            }
        }

        [HttpDelete]
        [Route("remove")]
        public ApiJsonResponseModel<ProductModel> RemoveProduct([FromQuery] int productId)
        {
            var product = db.Products.Find(productId);
            if (product == null)
                return new ApiJsonResponseModel<ProductModel> { Data = null, ErrorMessage = ErrorMessages.PRODUCT_NOT_FOUND, ResponseCode = HttpResponseCode.NOT_FOUND };
            db.Products.Remove(product);
            return new ApiJsonResponseModel<ProductModel> { Data = product.MapToProductModel() };
        }

        [HttpGet]
        [Route("image")]
        public ActionResult GetImage([FromQuery] string productId)
        {
            try
            {
                var path = Path.Combine(hostingEnvironment.WebRootPath, "Resources", "Images", "Products", $"{productId}");
                return PhysicalFile(path, "image/jpeg");
            } catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("hot-products")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> GetHotProducts()
        {
            var hotProducts = db.Products.AsNoTracking().Include(prod => prod.Category).OrderBy(prod => prod.InvoiceDetails.Count).Take(5);
            return new ApiJsonResponseModel<IEnumerable<ProductModel>> { Data = hotProducts.MapToProductModel() };
        }

        [HttpPost]
        [Route("search")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> SearchProducts([FromBody] SearchInputModel searchInput)
        {
            IEnumerable<Product> searchResult;
            if (searchInput.CategoryId == 0)
            {
                searchResult = db.Products.AsNoTracking().Include(prod => prod.Category).Where(prod => EF.Functions.Like(prod.Name, $"%{searchInput.Keyword}%"));
            }
            else
            {
                searchResult = db.Products.AsNoTracking().Include(prod => prod.Category).Where(prod => EF.Functions.Like(prod.Name, $"%{searchInput.Keyword}%") &&
                            prod.CategoryId == searchInput.CategoryId);
            }
            return new ApiJsonResponseModel<IEnumerable<ProductModel>>() { Data = searchResult.MapToProductModel() };
        }

        [HttpGet]
        [Route("new-products")]
        public ApiJsonResponseModel<IEnumerable<ProductModel>> GetNewProducts()
        {
            var newProducts = db.Products.AsNoTracking().Include(prod => prod.Category).Take(5).OrderBy(prod => prod.CreatedDate);
            return new ApiJsonResponseModel<IEnumerable<ProductModel>> { Data = newProducts.MapToProductModel() };
        }

        private async Task SaveImage(IFormFile file, string productId)
        {
            var folderName = Path.Combine(hostingEnvironment.WebRootPath, "Resources", "Images", "Products");
            if (file.Length > 0)
            {
                var fileName = productId;
                var filePath = Path.Combine(folderName, fileName);
                var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
