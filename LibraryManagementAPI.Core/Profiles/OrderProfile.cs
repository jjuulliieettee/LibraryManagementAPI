using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDto>()
                .ForMember(f => f.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(f => f.Reader,
                    opt => opt.MapFrom(src => src.Reader.Name))
                .ForMember(f => f.Librarian,
                opt => opt.MapFrom(src => src.Librarian.Name)); ;
            CreateMap<OrderCreateDto, Order>();
        }
    }
}
