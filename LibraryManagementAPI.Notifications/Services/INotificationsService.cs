using System.Threading.Tasks;
using LibraryManagementAPI.Notifications.Dtos;

namespace LibraryManagementAPI.Notifications.Services
{
    public interface INotificationsService
    {
        public Task AddBook(BookDto book);
        public Task RemoveBook(BookDto book);
        public Task OrderBook(BookDto book);
    }
}
