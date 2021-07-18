using Api.Models;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helper
{
    public static class CategoryHelper
    {
        public static IEnumerable<CategoryModel> MapToCategoryModel(this IEnumerable<Category> categories)
        {
            return categories.Select(item => MapToCategoryModel(item));
        }

        public static CategoryModel MapToCategoryModel(this Category category)
        {
            return new CategoryModel { Id = category.Id, Name = category.Name };
        }
    }
}
