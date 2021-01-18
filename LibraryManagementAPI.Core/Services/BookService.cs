using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Extensions;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        public BookService(IBookRepo bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<Book> AddAsync(Book book)
        {
            IEnumerable<Book> booksInDb = await _bookRepo.GetAllSimilarBooksAsync(book);

            if (booksInDb.Any())
            {
                throw new ApiException(MessagesResource.BOOK_ALREADY_EXISTS);
            }
            return await _bookRepo.AddAsync(book);
        }

        public async Task<Book> DeleteAsync(int id)
        {
            Book book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_FOUND, 404);
            }

            if (!book.IsAvailable)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_DELETABLE);
            }

            await _bookRepo.DeleteAsync(book);

            return book;
        }

        public async Task<Book> EditAsync(Book book)
        {
            Book bookOld = await _bookRepo.GetByIdToEditAsync(book.Id);
            if (bookOld == null)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_FOUND, 404);
            }

            if (!bookOld.IsAvailable)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_EDITABLE);
            }

            IEnumerable<Book> booksInDb = await _bookRepo.GetAllSimilarBooksAsync(book);

            if (booksInDb.Any())
            {
                throw new ApiException(MessagesResource.BOOK_ALREADY_EXISTS);
            }

            return await _bookRepo.EditAsync(book, false);
        }

        public async Task<IEnumerable<object>> GetAllAsync(BookQueryParamsDto queryParams)
        {
            var query = _bookRepo.GetAll()
                                 .Where(b => queryParams.IsAvailable == null ||
                                             b.IsAvailable == queryParams.IsAvailable)
                                 .Where(b => queryParams.Author == null || b.Author.Name.Contains(queryParams.Author))
                                 .Where(b => queryParams.Genre == null || b.Genre.Name.Contains(queryParams.Genre))
                                 .Where(b => queryParams.Year == null || b.YearOfPublishing == queryParams.Year)
                                 .ProjectTo<BookReadDto>(_mapper.ConfigurationProvider)
                                 .OrderByDynamic(queryParams.PropertyNameToOrder, queryParams.Ascending);
            
            if (!string.IsNullOrEmpty(queryParams.PropertyNameToGroup))
            {
                return (await query
                       .ToListAsync())
                       .GroupByDynamic(queryParams.PropertyNameToGroup);
            }

            return await query.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepo.GetByIdAsync(id);
        }

        public async Task<Book> ToggleAvailability(int id)
        {
            Book book = await _bookRepo.GetByIdToEditAsync(id);
            if (book == null)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_FOUND, 404);
            }

            book.IsAvailable = !book.IsAvailable;
            return await _bookRepo.EditAsync(book);
        }
    }
}
