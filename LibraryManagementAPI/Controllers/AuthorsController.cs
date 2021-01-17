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
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Librarian))]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(await _authorService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get([FromRoute] int id)
        {
            AuthorDto author = _mapper.Map<AuthorDto>(await _authorService.GetByIdAsync(id));

            if (author == null)
            {
                throw new ApiException(MessagesResource.AUTHOR_NOT_FOUND, 404);
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Post([FromBody] AuthorCreateDto authorCreateDto)
        {
            AuthorDto newAuthor = _mapper.Map<AuthorDto>(
                await _authorService.AddAsync(_mapper.Map<Author>(authorCreateDto))
            );
            return CreatedAtAction("Get", new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Put([FromRoute] int id, [FromBody] AuthorDto authorDto)
        {
            authorDto.Id = id;
            await _authorService.EditAsync(_mapper.Map<Author>(authorDto));
            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _authorService.DeleteAsync(id);
            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }
    }
}
