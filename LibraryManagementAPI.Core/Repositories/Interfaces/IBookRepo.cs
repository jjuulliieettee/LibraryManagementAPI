using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IBookRepo
    {
        IQueryable<Book> GetAll();
        Task<IEnumerable<Book>> GetAllSimilarBooksAsync(Book book);
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByIdToEditAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> EditAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
