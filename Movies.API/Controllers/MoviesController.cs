using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Data.Interfaces;
using Movies.Data.Models;


namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        public MoviesController(IMovieRepository movieRepository)
        { 
            _movieRepo = movieRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            try
            {
                return Ok(_movieRepo.GetAll());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            try
            {
                var movie = _movieRepo.GetMovieById(id);
                if (movie == null)
                {
                    return NotFound(new { msg = "Rezultat nije pronađen!" });
                }
                else return Ok(movie);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Nije moguće prikazati rezultate, dogodila se greška!" });
            }
        }
    }
}
