﻿using System;

namespace StrategyCorps.CodeSample.Models
{
    public class MovieResultDto
    {
        public decimal Popularity { get; set; }

        public int Id { get; set; }

        public decimal VoteAverage { get; set; }

        public string Overview { get; set; }

        public DateTime? FirstAirDate { get; set; }
        
        public string OriginalLanguage { get; set; }

        public int VoteCount { get; set; }

        public string Title { get; set; }

        public string OriginalTitle { get; set; }
    }
}
