using System;
using System.Linq;
using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookReadDto>()
                .ForMember(f => f.Author,
                    opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(f => f.Genre,
                    opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(f => f.IsAvailable,
                    opt => opt.MapFrom(src =>
                        !src.Orders.Any() || !src.Orders.Any(o => o.IsBorrowed || o.BorrowDate.Date >= DateTime.Today)));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookEditDto, Book>();
        }
    }
}
