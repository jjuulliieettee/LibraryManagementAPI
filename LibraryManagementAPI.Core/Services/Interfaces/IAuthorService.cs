using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<int> SaveAsync(string name);
        Task<Author> AddAsync(Author author);
        Task<Author> EditAsync(Author author);
        Task DeleteAsync(int id);
    }
}
