using AutoMapper;
using ECommerce.CartAPI.Models.DTO;
using ECommerce.CartAPI.Models;

namespace ECommerce.CartAPI.Mapping
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CartItems, CartItemsDTO>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
        }
    }
}
