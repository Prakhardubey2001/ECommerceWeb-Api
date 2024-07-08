using ECommerce.CartAPI.Models.DTO;
using ECommerce.CartAPI.Service.Iservice;
using Newtonsoft.Json;

namespace ECommerce.CartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ProductService(IHttpClientFactory clientFactory)
        {
            httpClientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var client = httpClientFactory.CreateClient("Product");
            var products = await client.GetAsync($"api/product");
            var apiContent = await products.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDTO>();
        }
    }
}
