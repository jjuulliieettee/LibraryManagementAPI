using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepo _genreRepo;
        public GenreService(IGenreRepo genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            if (await _genreRepo.GetByNameAsync(genre.Name) != null)
            {
                throw new ApiException(MessagesResource.GENRE_ALREADY_EXISTS);
            }
            return await _genreRepo.AddAsync(genre);
        }

        public async Task DeleteAsync(int id)
        {
            Genre genre = await _genreRepo.GetByIdAsync(id);

            if (genre == null)
            {
                throw new ApiException(MessagesResource.GENRE_NOT_FOUND, 404);
            }

            if (genre.Books != null && genre.Books.Any())
            {
                throw new ApiException(MessagesResource.GENRE_NOT_DELETABLE);
            }
            await _genreRepo.DeleteAsync(genre);
        }

        public async Task<Genre> EditAsync(Genre genre)
        {
            Genre genreOld = await _genreRepo.GetByIdAsync(genre.Id);
            if (genreOld == null)
            {
                throw new ApiException(MessagesResource.GENRE_NOT_FOUND, 404);
            }

            if (genreOld.Books != null && genreOld.Books.Any())
            {
                throw new ApiException(MessagesResource.GENRE_NOT_EDITABLE);
            }

            Genre genreInDb = await _genreRepo.GetByNameAsync(genre.Name);
            if (genreInDb != null && genreInDb.Id != genre.Id)
            {
                throw new ApiException(MessagesResource.GENRE_ALREADY_EXISTS);
            }

            return await _genreRepo.EditAsync(genre);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _genreRepo.GetAllAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _genreRepo.GetByIdAsync(id);
        }

        public async Task<int> SaveAsync(string name)
        {
            Genre genreInDb = await _genreRepo.GetByNameAsync(name);
            if (genreInDb != null)
            {
                return genreInDb.Id;
            }

            return (await _genreRepo.AddAsync(new Genre
            {
                Name = name
            })).Id;
        }
    }
}
