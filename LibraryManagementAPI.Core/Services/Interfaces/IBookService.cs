using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<object>> GetAllAsync(BookQueryParamsDto queryParams);
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> EditAsync(Book book);
        Task DeleteAsync(int id);
        Task<Book> ToggleAvailability(int id);
    }
}
