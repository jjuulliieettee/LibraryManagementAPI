using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
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
    [Authorize(Roles = nameof(UserRole.Librarian))]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly INotificationsService _notificationsService;

        public BooksController(IBookService bookService, IMapper mapper, IAuthorService authorService, IGenreService genreService, 
            INotificationsService notificationsService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _mapper = mapper;
            _notificationsService = notificationsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery]BookQueryParamsDto queryParams)
        {
            return Ok(await _bookService.GetAllAsync(queryParams));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookReadDto>> Get([FromRoute] int id)
        {
            BookReadDto book = _mapper.Map<BookReadDto>(await _bookService.GetByIdAsync(id));

            if (book == null)
            {
                throw new ApiException(MessagesResource.BOOK_NOT_FOUND, 404);
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> Post([FromBody] BookCreateDto bookCreateDto)
        {
            if (!string.IsNullOrEmpty(bookCreateDto.NewAuthorName))
            {
                bookCreateDto.AuthorId = await _authorService.SaveAsync(bookCreateDto.NewAuthorName);
            }

            if (!string.IsNullOrEmpty(bookCreateDto.NewGenreName))
            {
                bookCreateDto.GenreId = await _genreService.SaveAsync(bookCreateDto.NewGenreName);
            }

            bookCreateDto.IsAvailable = true;

            BookReadDto newBook = _mapper.Map<BookReadDto>(
                await _bookService.AddAsync(_mapper.Map<Book>(bookCreateDto))
            );

            await _notificationsService.AddBook(new BookDto
            {
                Author = newBook.Author,
                Title = newBook.Title
            });

            return CreatedAtAction("Get", new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] BookEditDto bookEditDto)
        {
            bookEditDto.Id = id;
            await _bookService.EditAsync(_mapper.Map<Book>(bookEditDto));
            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Book book = await _bookService.DeleteAsync(id);

            await _notificationsService.RemoveBook(new BookDto
            {
                Author = book.Author.Name,
                Title = book.Title
            });

            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }
    }
}
