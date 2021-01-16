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
                    opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookEditDto, Book>();
        }
    }
}
