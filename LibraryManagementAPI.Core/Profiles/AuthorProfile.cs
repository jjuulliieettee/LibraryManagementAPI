using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorDto, Author>();
        }
    }
}
