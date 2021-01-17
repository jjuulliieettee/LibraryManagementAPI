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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        public AuthorService(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> AddAsync(Author author)
        {
            if (await _authorRepo.GetByNameAsync(author.Name) != null)
            {
                throw new ApiException(MessagesResource.AUTHOR_ALREADY_EXISTS);
            }
            return await _authorRepo.AddAsync(author);
        }

        public async Task DeleteAsync(int id)
        {
            Author author = await _authorRepo.GetByIdAsync(id);

            if (author == null)
            {
                throw new ApiException(MessagesResource.AUTHOR_NOT_FOUND, 404);
            }

            if (author.Books != null && author.Books.Any())
            {
                throw new ApiException(MessagesResource.AUTHOR_NOT_DELETABLE);
            }
            await _authorRepo.DeleteAsync(author);
        }

        public async Task<Author> EditAsync(Author author)
        {
            Author authorOld = await _authorRepo.GetByIdAsync(author.Id);
            if (authorOld == null)
            {
                throw new ApiException(MessagesResource.AUTHOR_NOT_FOUND, 404);
            }

            if (authorOld.Books != null && authorOld.Books.Any())
            {
                throw new ApiException(MessagesResource.AUTHOR_NOT_EDITABLE);
            }

            Author authorInDb = await _authorRepo.GetByNameAsync(author.Name);
            if (authorInDb != null && authorInDb.Id != author.Id)
            {
                throw new ApiException(MessagesResource.AUTHOR_ALREADY_EXISTS);
            }

            return await _authorRepo.EditAsync(author);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepo.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _authorRepo.GetByIdAsync(id);
        }

        public async Task<int> SaveAsync(string name)
        {
            Author authorInDb = await _authorRepo.GetByNameAsync(name);
            if (authorInDb != null)
            {
                return authorInDb.Id;
            }

            return (await _authorRepo.AddAsync(new Author
            {
                Name = name
            })).Id;
        }
    }
}
