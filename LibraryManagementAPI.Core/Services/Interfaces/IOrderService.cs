using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<object>> GetAllAsync(OrderQueryParamsDto queryParams);
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task<int> DeleteAsync(int id);
        Task<Order> ConfirmOrderAsync(int orderId, int librarianId);
        Task<Order> ReturnOrderAsync(int orderId);
    }
}
