using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Repositories
{
    public class GenreRepo : IGenreRepo
    {
        private readonly LibraryContext _context;
        public GenreRepo(LibraryContext context)
        {
            _context = context;
        }
        
        public async Task<Genre> AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task DeleteAsync(Genre genre)
        {
            _context.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> EditAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres
                                 .Include(genre => genre.Books)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(genre => genre.Id == id);
        }

        public async Task<Genre> GetByNameAsync(string name)
        {
            return await _context.Genres
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(genre => genre.Name == name);
        }
    }
}
