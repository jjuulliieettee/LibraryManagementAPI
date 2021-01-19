using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Extensions;
using LibraryManagementAPI.Core.Repositories.Interfaces;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public OrderService(IOrderRepo orderRepo, IMapper mapper, IBookService bookService)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<Order> AddAsync(Order order)
        {
            if (order.BorrowDate.Date < DateTime.Today)
            {
                throw new ApiException(MessagesResource.INVALID_INPUT);
            }

            order.IsBorrowed = false;

            IEnumerable<Order> ordersInDb = await _orderRepo.GetAllSimilarOrdersAsync(order);

            if (ordersInDb.Any(o => o.IsBorrowed || o.BorrowDate.Date >= DateTime.Today))
            {
                throw new ApiException(MessagesResource.BOOK_ALREADY_ORDERED);
            }

            List<Order> ordersToDelete = ordersInDb
                                         .Where(o => !o.IsBorrowed && o.BorrowDate.Date < DateTime.Today && o.ReturnDate == null)
                                         .ToList();
            if (ordersToDelete.Any())
            {
                await _orderRepo.DeleteManyAsync(ordersToDelete);
                await _bookService.ToggleAvailability(ordersToDelete.FirstOrDefault().BookId);
            }

            return await _orderRepo.AddAsync(order);
        }

        public async Task<Order> ConfirmOrderAsync(int orderId, int librarianId)
        {
            Order orderInDb = await _orderRepo.GetByIdToEditAsync(orderId);
            if (orderInDb == null)
            {
                throw new ApiException(MessagesResource.ORDER_NOT_FOUND, 404);
            }

            orderInDb.LibrarianId = librarianId;
            orderInDb.IsBorrowed = true;

            return await _orderRepo.EditAsync(orderInDb);
        }

        public async Task<int> DeleteAsync(int id)
        {
            Order order = await _orderRepo.GetByIdToEditAsync(id);

            if (order == null)
            {
                throw new ApiException(MessagesResource.ORDER_NOT_FOUND, 404);
            }

            int bookId = order.BookId;

            if (order.IsBorrowed || order.ReturnDate != null)
            {
                throw new ApiException(MessagesResource.ORDER_NOT_DELETABLE);
            }

            await _orderRepo.DeleteAsync(order);

            return bookId;
        }

        public async Task<IEnumerable<object>> GetAllAsync(OrderQueryParamsDto queryParams)
        {
            var query = _orderRepo.GetAll()
                                  .Where(o => queryParams.IsBorrowed == null || queryParams.IsBorrowed == o.IsBorrowed)
                                 .Where(o => queryParams.Reader == null || o.Reader.Name.Contains(queryParams.Reader))
                                 .Where(o => queryParams.Librarian == null || o.Librarian.Name.Contains(queryParams.Librarian))
                                  .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
                                 .OrderByDynamic(queryParams.PropertyNameToOrder, queryParams.Ascending);

            if (!string.IsNullOrEmpty(queryParams.PropertyNameToGroup))
            {
                return (await query
                        .ToListAsync())
                    .GroupByDynamic(queryParams.PropertyNameToGroup);
            }

            return await query.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepo.GetByIdAsync(id);
        }

        public async Task<Order> ReturnOrderAsync(int orderId)
        {
            Order orderInDb = await _orderRepo.GetByIdToEditAsync(orderId);
            if (orderInDb == null)
            {
                throw new ApiException(MessagesResource.ORDER_NOT_FOUND, 404);
            }

            orderInDb.ReturnDate = DateTime.Now;
            orderInDb.IsBorrowed = false;

            orderInDb = await _orderRepo.EditAsync(orderInDb);

            return orderInDb;
        }
    }
}
