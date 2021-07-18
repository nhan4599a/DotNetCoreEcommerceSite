using Api.Helper;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AssignmentContext db;

        public CategoriesController()
        {
            db = new AssignmentContext();
        }

        [HttpGet]
        [Route("all")]
        public ApiJsonResponseModel<IEnumerable<CategoryModel>> getAll()
        {
            return new ApiJsonResponseModel<IEnumerable<CategoryModel>>
            {
                Data = db.Categories.MapToCategoryModel()
            };
        }

        [HttpPost]
        [Route("add")]
        public ApiJsonResponseModel<CategoryModel> AddCategory([FromBody] CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ApiJsonResponseModel<CategoryModel> { Data = null, ErrorMessage = "Parameter does not meet condition", ResponseCode = 400 };
            }
            try
            {
                Category category = new() { Name = model.Name };
                db.Categories.Add(category);
                db.SaveChanges();
                return new ApiJsonResponseModel<CategoryModel> { Data = category.MapToCategoryModel() };
            }
            catch (Exception e)
            {
                return new ApiJsonResponseModel<CategoryModel> { Data = null, ErrorMessage = e.Message, ResponseCode = 500 };
            }
        }
    }
}
