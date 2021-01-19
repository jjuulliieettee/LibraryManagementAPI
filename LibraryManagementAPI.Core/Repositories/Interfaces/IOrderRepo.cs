using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Repositories.Interfaces
{
    public interface IOrderRepo
    {
        IQueryable<Order> GetAll();
        Task<IEnumerable<Order>> GetAllSimilarOrdersAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task<Order> GetByIdToEditAsync(int id);
        Task<Order> AddAsync(Order order);
        Task<Order> EditAsync(Order order);
        Task DeleteAsync(Order order);
        Task DeleteManyAsync(List<Order> orders);
    }
}
