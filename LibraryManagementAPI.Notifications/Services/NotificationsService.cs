using System;
using System.Threading.Tasks;
using LibraryManagementAPI.Notifications.Dtos;
using LibraryManagementAPI.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManagementAPI.Notifications.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        public NotificationsService(IHubContext<NotificationsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddBook(BookDto book)
        {
            await _hubContext.Clients.All.SendAsync("AddBook", new
                { title = book.Title, author = book.Author, date = DateTime.Now }
            );
        }

        public async Task RemoveBook(BookDto book)
        {
            await _hubContext.Clients.All.SendAsync("RemoveBook", new
                { title = book.Title, author = book.Author, date = DateTime.Now }
            );
        }

        public async Task OrderBook(BookDto book)
        {
            await _hubContext.Clients.All.SendAsync("OrderBook", new
                { title = book.Title, author = book.Author, date = DateTime.Now }
            );
        }
    }
}
