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
                    throw new ArgumentNullException();
                }
                else return Ok(movie);
            }
            catch (ArgumentNullException error)
            {
                return NotFound("Rezultat nije pronađen!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nije moguće prikazati rezultate, dogodila se greška!");
            }
        }
        [HttpPost]
        public ActionResult PostMovie(Movie new_movie)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _movieRepo.InsertMovie(new_movie);
                return Ok("Zapis je kreiran.");
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, Movie update_movie)
        {
            try
            {
                if (update_movie.Id != id)
                {
                    throw new BadHttpRequestException("Parametri Id se ne poklapaju!");
                }
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Podaci nisu validni");
                }
                if (_movieRepo.GetMovieById(id) == null)
                {
                    throw new ArgumentNullException();
                }
                return Ok(_movieRepo.UpdateMovie(update_movie));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Rezultat nije pronađen!");
            }
            catch (BadHttpRequestException error)
            {
                return BadRequest(error.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nije moguće prikazati rezultate, dogodila se greška!");
            }
        }
    }
}
