﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly LibraryContext _context;
        public BookRepo(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        
        public async Task DeleteAsync(Book book)
        {
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> EditAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public IQueryable<Book> GetAll()
        {
            return _context.Books
                           .Include(b => b.Author)
                           .Include(b => b.Genre)
                           .Include(b => b.Orders);

        }

        public async Task<IEnumerable<Book>> GetAllSimilarBooksAsync(Book book)
        {
            return await _context.Books
                                 .Where(b => book.Title == b.Title 
                                             && book.AuthorId == b.AuthorId 
                                             && book.GenreId == b.GenreId
                                             && book.YearOfPublishing == b.YearOfPublishing)
                                 .Where(b => book.Id != b.Id)
                                 .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                                 .FirstOrDefaultAsync(book => book.Id == id);
        }
    }
}