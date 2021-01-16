using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
