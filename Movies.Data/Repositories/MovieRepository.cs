using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly algebramssqlhost_moviesContext _context;
        public MovieRepository(algebramssqlhost_moviesContext context)
        { 
            _context = context;
        }

        public Movie DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.Find(id);
        }

        public Movie InsertMovie(Movie movie)
        {
            var movies_sorted = _context.Movies.OrderBy(key=> key.Id);
            movie.Id = movies_sorted.Last().Id + 1;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public IEnumerable<Movie> QuerryStringFilter(string s, string orderby, int per_page)
        {
            throw new NotImplementedException();
        }

        public Movie UpdateMovie(Movie movie)
        {
            var searchMovie = GetMovieById(movie.Id);
            searchMovie.Title = movie.Title;
            searchMovie.Genre = movie.Genre;
            searchMovie.ReleaseYear = movie.ReleaseYear;         
            _context.SaveChanges();
            return movie;
        }
    }
}