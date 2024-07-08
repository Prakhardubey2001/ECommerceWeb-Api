using ECommerce.CartAPI.Models.DTO;

namespace ECommerce.CartAPI.Service.Iservice
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
