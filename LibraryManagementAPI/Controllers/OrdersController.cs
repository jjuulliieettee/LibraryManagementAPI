using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Extensions;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Enums;
using LibraryManagementAPI.Data.Models;
using LibraryManagementAPI.Notifications.Dtos;
using LibraryManagementAPI.Notifications.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly INotificationsService _notificationsService;
        public OrdersController(IOrderService orderService, IMapper mapper, IBookService bookService, 
            INotificationsService notificationsService)
        {
            _orderService = orderService;
            _bookService = bookService;
            _mapper = mapper;
            _notificationsService = notificationsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] OrderQueryParamsDto queryParams)
        {
            return Ok(await _orderService.GetAllAsync(queryParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> Get([FromRoute] int id)
        {
            OrderReadDto order = _mapper.Map<OrderReadDto>(await _orderService.GetByIdAsync(id));

            if (order == null)
            {
                throw new ApiException(MessagesResource.ORDER_NOT_FOUND, 404);
            }

            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Reader))]
        public async Task<ActionResult<OrderReadDto>> Post([FromBody] OrderCreateDto orderCreateDto)
        {
            orderCreateDto.ReaderId = User.GetUserId();

            OrderReadDto newOrder = _mapper.Map<OrderReadDto>(
                await _orderService.AddAsync(_mapper.Map<Order>(orderCreateDto))
            );

            Book book = await _bookService.ToggleAvailability(newOrder.BookId);

            await _notificationsService.OrderBook(new BookDto
            {
                Author = (await _bookService.GetByIdAsync(book.Id)).Author.Name,
                Title = book.Title
            });

            return CreatedAtAction("Get", new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("TakeOrder/{id}")]
        [Authorize(Roles = nameof(UserRole.Librarian))]
        public async Task<ActionResult> TakeOrder([FromRoute] int id)
        {
            int librarianId = User.GetUserId();

            await _orderService.ConfirmOrderAsync(id, librarianId);

            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }

        [HttpPut("ReturnOrder/{id}")]
        [Authorize(Roles = nameof(UserRole.Librarian))]
        public async Task<ActionResult> ReturnOrder([FromRoute] int id)
        {
            Order order = await _orderService.ReturnOrderAsync(id);

            await _bookService.ToggleAvailability(order.BookId);

            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Reader))]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int bookId = await _orderService.DeleteAsync(id);

            await _bookService.ToggleAvailability(bookId);

            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }
    }
}
