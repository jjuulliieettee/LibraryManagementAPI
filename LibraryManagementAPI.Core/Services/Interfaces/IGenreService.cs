using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task<int> SaveAsync(string name);
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> EditAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}
