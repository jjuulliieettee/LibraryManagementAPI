using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.Core.Dtos;
using LibraryManagementAPI.Core.Exceptions;
using LibraryManagementAPI.Core.Resources;
using LibraryManagementAPI.Core.Services.Interfaces;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(await _genreService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> Get([FromRoute] int id)
        {
            GenreDto genre = _mapper.Map<GenreDto>(await _genreService.GetByIdAsync(id));

            if (genre == null)
            {
                throw new ApiException(MessagesResource.GENRE_NOT_FOUND, 404);
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Post([FromBody] GenreCreateDto genreCreateDto)
        {
            GenreDto newGenre = _mapper.Map<GenreDto>(
                await _genreService.AddAsync(_mapper.Map<Genre>(genreCreateDto))
            );
            return CreatedAtAction("Get", new { id = newGenre.Id }, newGenre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDto>> Put([FromRoute] int id, [FromBody] GenreDto genreDto)
        {
            genreDto.Id = id;
            await _genreService.EditAsync(_mapper.Map<Genre>(genreDto));
            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _genreService.DeleteAsync(id);
            return Ok(new { message = MessagesResource.SUCCESS_MESSAGE });
        }
    }
}
