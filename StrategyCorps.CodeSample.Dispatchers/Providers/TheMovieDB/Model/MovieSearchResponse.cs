﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    //TODO: Consider combining into an abstract class instead of POCOs may be too complex though
    public class MovieSearchResponse
    {
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MovieResult> Results { get; set; }

        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public int Page { get; set; }

        [JsonProperty("total_results", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPages { get; set; }
    }
}
