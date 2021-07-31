using Newtonsoft.Json;
using SharedModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace EcommerceSite.Helper
{
    public class ApiCaller
    {
        private readonly HttpClient client;

        private static ApiCaller instance;

        private const string BASE_URL = "https://localhost:44381/api";

        private ApiCaller()
        {
            client = new HttpClient();
        }

        public static ApiCaller GetInstance()
        {
            if (instance == null)
                instance = new ApiCaller();
            return instance;
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            string response = await client.GetStringAsync(BASE_URL + "/users/all");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<UserModel>>>(response);
            return responseModel.Data;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            string response = await client.GetStringAsync(BASE_URL + "/products/all");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<ProductModel>>>(response);
            return responseModel.Data;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            string response = await client.GetStringAsync(BASE_URL + "/categories/all");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<CategoryModel>>>(response);
            return responseModel.Data;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsOfCategory(int categoryId)
        {
            string response = await client.GetStringAsync(BASE_URL + $"/products/find?categoryId={categoryId}");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<ProductModel>>>(response);
            return responseModel.Data;
        }

        public async Task<IEnumerable<ProductModel>> GetHotProducts()
        {
            string response = await client.GetStringAsync(BASE_URL + "/products/hot-products");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<ProductModel>>>(response);
            return responseModel.Data;
        }

        public async Task<IEnumerable<ProductModel>> GetNewProducts()
        {
            string response = await client.GetStringAsync(BASE_URL + "/products/new-products");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<ProductModel>>>(response);
            return responseModel.Data;
        }

        public static string GetImageUrl(string productId)
        {
            return $"{BASE_URL}/products/image?productId={WebUtility.UrlEncode(productId)}";
        }

        public async Task<IEnumerable<ProductModel>> SearchProducts(SearchInputModel searchInput)
        {
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(searchInput));
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(BASE_URL + "/products/search", postContent);
            string content = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<IEnumerable<ProductModel>>>(content);
            return responseModel.Data;
        }

        public async Task<ProductModel> GetProductDetail(string productId)
        {
            var response = await client.GetStringAsync(BASE_URL + $"/products/get?id={WebUtility.UrlEncode(productId)}");
            var responseModel = JsonConvert.DeserializeObject<ApiJsonResponseModel<ProductModel>>(response);
            return responseModel.Data;
        }
    }
}
