using SharedModel;
using System.Collections.Generic;

namespace EcommerceSite.Models
{
    public class SearchResultPageModel : RequiredFieldModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
