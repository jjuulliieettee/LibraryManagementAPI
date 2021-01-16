using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreDto, Genre>();
        }
    }
}
