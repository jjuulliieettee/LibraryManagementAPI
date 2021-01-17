using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly LibraryContext _context;
        public OrderRepo(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> EditAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders
                           .Include(o => o.Book)
                           .Include(o => o.Librarian)
                           .Include(o => o.Reader);
        }

        public async Task<IEnumerable<Order>> GetAllSimilarOrdersAsync(Order order)
        {
            return await _context.Orders
                                 .Include(o => o.Book)
                                 .Where(o => order.BookId == o.BookId
                                             && !o.Book.IsAvailable)
                                 .Where(o => order.Id != o.Id)
                                 .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                                 .Include(o => o.Book)
                                 .Include(o => o.Librarian)
                                 .Include(o => o.Reader)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<Order> GetByIdToEditAsync(int id)
        {
            return await _context.Orders
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(order => order.Id == id);
        }
    }
}
