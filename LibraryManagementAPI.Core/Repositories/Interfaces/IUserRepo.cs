using System;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
