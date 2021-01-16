using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IAuthorRepo
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> GetByNameAsync(string name);
        Task<Author> AddAsync(Author author);
        Task<Author> EditAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
