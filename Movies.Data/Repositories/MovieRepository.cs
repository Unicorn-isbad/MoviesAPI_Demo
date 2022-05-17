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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> QuerryStringFilter(string s, string orderby, int per_page)
        {
            throw new NotImplementedException();
        }

        public Movie UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}