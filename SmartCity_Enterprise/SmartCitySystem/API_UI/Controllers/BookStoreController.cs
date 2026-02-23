using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.BooksDtos;
using Services.DTOs.BookStoreAnalizeDtos;
using Services.DTOs.BookStoreItemsDto;
using Services.DTOs.FilmDtos;
using Services.DTOs.GradDtos;
using System.Numerics;

namespace API_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreService _service;
        public BookStoreController(BookStoreService service)
        {
            _service = service; 
        }

        [HttpGet("AllData")]
        public async Task<ActionResult<IEnumerable<BookStoreItemsReadDto>>> GetAllData()
        {
            var data = await _service.GetReportData();
            return Ok(data);

        }

        [HttpGet("AllBooks")]
        public async Task<ActionResult<IEnumerable<BooksReadDto>>> GetBooks()
        {
            var books = await _service.GetBooks();
            return Ok(books);
        }

        [HttpGet("AllMovies")]
        public async Task<ActionResult<IEnumerable<FilmReadDto>>> GetMovies()
        {
            var movies = await _service.GetMovies();
            return Ok(movies);

        }

        [HttpGet("AllCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> GetAllCities()
        {
            var cities = await _service.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("BooksByAuthor")]
        public async Task<ActionResult<IEnumerable<GroupedByAuthorDto>>> BooksByAuthor()
        {
            return await _service.GroupedByAuthor();
        }


        [HttpGet("LongBooks")]
        public async Task<ActionResult<IEnumerable<BooksReadDto>>> LongBooks()
        {
            var longBooks = await _service.LongBooks();
            if (longBooks.Any() == false) return NotFound("There is no book longer than 300 pages");
 
            return Ok(longBooks);
        }

        [HttpGet("MoviesByDirector")]
        public async Task<ActionResult<IEnumerable<MoviesByDirectorDto>>> GetMoviesByDirector()
        {
            return Ok(await _service.GroupedByDirector());
        }



        [HttpGet("MovieAvailability/{MovieName}")]
        public async Task<bool> GetAvailability(string MovieName)
        {
            return await _service.GetDostupnost(MovieName);
        }



        [HttpPost("AddBook", Name = "AddBook")]
        public async Task<ActionResult<BooksCreateDto>> AddNewBook(BooksCreateDto obj)
        {
                await _service.AddNewBook(obj);
                return CreatedAtRoute("AddBook", obj);


        }


        [HttpPost("AddMovie", Name = "AddMovie")]
        public async Task<ActionResult<FilmCreateDto>> AddNewMovie(FilmCreateDto obj)
        {
                await _service.AddNewFilm(obj);
                return CreatedAtRoute("AddMovie", obj);


        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult> UpdateBook(int id, BooksUpdateDto obj)
        {

                await _service.UpdateBook(id, obj);
                return NoContent();
          
        }

        [HttpPut("UpdateMovie/{id}")]
        public async Task<ActionResult> UpdateMovie(int id, FilmUpdateDto obj)
        {
         
                await _service.UpdateFilm(id, obj);
                return NoContent();
          
        }



        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
           
                await _service.DeleteArtikal(id);
                return NoContent();
          
        }

        [HttpDelete("DeleteMovie/{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
          
                await _service.DeleteArtikal(id);
                return NoContent();
          
        }

    }
}
