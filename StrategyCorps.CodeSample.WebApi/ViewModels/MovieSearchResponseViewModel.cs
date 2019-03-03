using System.Collections.Generic;

namespace StrategyCorps.CodeSample.WebApi.ViewModels
{
    /// <summary>
    /// TelevisionSearchResponseViewModel
    /// </summary>
    public class MovieSearchResponseViewModel
    {
        /// <summary>
        /// Results
        /// </summary>
        public IList<MovieResultViewModel> Results { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Total Results
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages { get; set; }
    }
}
