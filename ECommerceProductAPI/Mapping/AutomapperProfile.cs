using AutoMapper;
using ECommerceProductAPI.Model.DTO;
using ECommerceProductAPI.Model;

namespace ECommerceProductAPI.Mapping
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

        }
    }
}
