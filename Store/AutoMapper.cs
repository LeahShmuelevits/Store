using AutoMapper;
using Entities;
using DTO;
namespace Store
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, GetUserDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, GetOrderDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderItem,OrderItemDTO > ();

        }

    }
}
