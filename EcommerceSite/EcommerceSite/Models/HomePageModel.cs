using SharedModel;
using System.Collections.Generic;

namespace EcommerceSite.Models
{
    public class HomePageModel : RequiredFieldModel
    {
        public IEnumerable<ProductModel> HotProducts { get; set; }
        public IEnumerable<ProductModel> NewProducts { get; set; }
    }
}
