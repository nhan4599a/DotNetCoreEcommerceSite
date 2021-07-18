using Api.Models;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helper
{
    public static class ProductHelper
    {
        public static IEnumerable<ProductModel> MapToProductModel(this IEnumerable<Product> products)
        {
            return products.Select(item => MapToProductModel(item));
        }

        public static ProductModel MapToProductModel(this Product product)
        {
            return new ProductModel
            {
                Id = product.Id.ToString(),
                CategoryName = product.Category.Name,
                Name = product.Name,
                Description = product.Decription,
                Price = product.Price
            };
        }
    }
}
