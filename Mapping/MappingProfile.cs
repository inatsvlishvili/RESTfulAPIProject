using AutoMapper;
using RESTfulAPIProject.Dtos;
using RESTfulAPIProject.Models;


namespace RESTfulAPIProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();


        }
    }
}
