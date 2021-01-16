using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly LibraryContext _context;
        public AuthorRepo(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }
        
        public async Task DeleteAsync(Author author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> EditAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(author => author.Id == id);
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            return await _context.Authors
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(author => author.Name == name);
        }
    }
}
