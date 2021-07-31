using System.Collections.Generic;
using System.Linq;

namespace EcommerceSite.Helper
{
    public static class PaginationHelper
    {
        public static IEnumerable<TModel> Paginate<TModel>(this IEnumerable<TModel> source, int pageNumber, int pageSize)
        {
            return source.Skip(pageNumber - 1).Take(pageSize);
        }
    }
}
