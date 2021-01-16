using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IGenreRepo
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task<Genre> GetByNameAsync(string name);
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> EditAsync(Genre genre);
        Task DeleteAsync(Genre genre);
    }
}
