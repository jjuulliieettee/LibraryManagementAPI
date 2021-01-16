using System.Threading.Tasks;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly LibraryContext _context;
        public UserRepo(LibraryContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
