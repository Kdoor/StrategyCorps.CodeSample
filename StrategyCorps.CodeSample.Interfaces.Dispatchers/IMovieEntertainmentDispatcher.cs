using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Dispatchers
{
    public interface IMovieEntertainmentDispatcher
    {
        MovieSearchResponseDto GetMoviesByQuery(string query);

        MovieSearchResponseDto GetSimilarMoviesById(int id);
    }
}