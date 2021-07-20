using SharedModel;
using System.Collections.Generic;

namespace EcommerceSite.Models
{
    public class HomePageModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<ProductModel> HotProducts { get; set; }
        public IEnumerable<ProductModel> NewProducts { get; set; }
    }
}
