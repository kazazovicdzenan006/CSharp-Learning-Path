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
        private readonly IMapper _map;
        public BookStoreController(BookStoreService service, IMapper map)
        {
            _map = map;
            _service = service; 
        }

        [HttpGet("AllData")]
        public async Task<ActionResult<IEnumerable<BibliotekaArtikal>>> GetAllData()
        {
            var data = await _service.GetReportData();
            var dataDto = _map.Map<IEnumerable<BookStoreItemsReadDto>>(data);
            return Ok(dataDto);

        }

        [HttpGet("AllBooks")]
        public async Task<ActionResult<IEnumerable<BooksReadDto>>> GetBooks()
        {
            var books = await _service.GetBooks();
            var booksDto = _map.Map<IEnumerable<BooksReadDto>>(books);
            return Ok(booksDto);
        }

        [HttpGet("AllMovies")]
        public async Task<ActionResult<IEnumerable<FilmReadDto>>> GetMovies()
        {
            var movies = await _service.GetMovies();
            var moviesDto = _map.Map<IEnumerable<FilmReadDto>>(movies);
            return Ok(moviesDto);

        }

        [HttpGet("AllCities")]
        public async Task<ActionResult<IEnumerable<GradReadDto>>> GetAllCities()
        {
            var cities = await _service.GetAllCitiesAsync();
            var citiesDto = _map.Map<IEnumerable<GradReadDto>>(cities);
            return Ok(citiesDto);
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
            var booksDto = _map.Map<IEnumerable<BooksReadDto>>(longBooks);
            return Ok(booksDto);
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
            var book = _map.Map<Knjiga>(obj);
      
         
                await _service.AddNewBook(book);
                var readDto = _map.Map<BooksReadDto>(book);
                return CreatedAtRoute("AddBook", readDto);


        }


        [HttpPost("AddMovie", Name = "AddMovie")]
        public async Task<ActionResult<FilmCreateDto>> AddNewMovie(FilmCreateDto obj)
        {
            var movie = _map.Map<Film>(obj);

                await _service.AddNewFilm(movie);
                var readDto = _map.Map<FilmReadDto>(movie);
                return CreatedAtRoute("AddMovie", readDto);


        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult> UpdateBook(int id, BooksUpdateDto obj)
        {
            var book = _map.Map<Knjiga>(obj);
          
                await _service.UpdateArtikal(id, book);
                return NoContent();
          
        }

        [HttpPut("UpdateMovie/{id}")]
        public async Task<ActionResult> UpdateMovie(int id, FilmUpdateDto obj)
        {
            var movie = _map.Map<Film>(obj);
         
                await _service.UpdateArtikal(id, movie);
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
