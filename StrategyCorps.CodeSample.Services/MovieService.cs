using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieEntertainmentDispatcher _entertainmentDispatcher;
 
        public MovieService(IMovieEntertainmentDispatcher entertainmentDispatcher)
        {
            _entertainmentDispatcher = entertainmentDispatcher;
        }

        /// <summary>
        /// Gets movies that meet the query criteria
        /// </summary>
        /// <param name="query">The criteria used to search for movies</param>
        /// <returns cref="MovieSearchResponseDto"></returns>
        public MovieSearchResponseDto GetMoviesByQuery(string query)
        {
            return _entertainmentDispatcher.GetMoviesByQuery(query);
        }

        /// <summary>
        /// Gets movies that are similar to the movies whose id is passed in.
        /// </summary>
        /// <param name="id">The id of the movie used to find similar movies.</param>
        /// <returns cref="MovieSearchResponseDto"></returns>
        public MovieSearchResponseDto GetSimilarMoviesById(int id)
        {
            return _entertainmentDispatcher.GetSimilarMoviesById(id);
        }
    }
}
