using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using NUnit.Framework;
using StrategyCorps.CodeSample.Dispatchers.MappingProfiles;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.Tests.MappingProfiles
{
    [TestFixture]
    public class TheMovieDbMappingProfileTests
    {
        private IMapper _mapper;

        //TODO: Consider adding test categories

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<TheMovieDbMappingProfile>(); });
            _mapper = config.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            _mapper = null;
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionSearchResponse_Returns_TelevisionSearchResponseDTO()
        {
            var currentDateTime = DateTime.Now;
            var zeroSecondDate = currentDateTime.AddSeconds(-currentDateTime.Second);

            var televisionResults = Builder<TelevisionResult>.CreateListOfSize(5).All()
                .With(x => x.FirstAirDate = zeroSecondDate.ToString(CultureInfo.InvariantCulture)).Build().ToList();
            var televisionSearchResponse = Builder<TelevisionSearchResponse>.CreateNew()
                                                                                  .With(x => x.Results = televisionResults).Build();

            var televisionResultsDto = televisionResults.Select(televisionResult => new TelevisionResultDto
            {
                FirstAirDate = DateTime.Parse(televisionResult.FirstAirDate),
                Id = televisionResult.Id,
                Name = televisionResult.Name,
                OriginalLanguage = televisionResult.OriginalLanguage,
                OriginalName = televisionResult.OriginalName,
                Overview = televisionResult.Overview,
                Popularity = televisionResult.Popularity,
                VoteAverage = televisionResult.VoteAverage,
                VoteCount = televisionResult.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseDto>.CreateNew()
                                                                           .With(x => x.Page = televisionSearchResponse.Page)
                                                                           .With(x => x.TotalPages = televisionSearchResponse.TotalPages)
                                                                           .With(x => x.TotalResults = televisionSearchResponse.TotalResults)
                                                                           .With(x => x.Results = televisionResultsDto).Build();

            var actualResult = _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDto>(televisionSearchResponse);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionResult_Returns_TelevisionResultDTO()
        {
            var televisionResult = Builder<TelevisionResult>.CreateNew().With(x => x.FirstAirDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)).Build();
            var expectedResult = Builder<TelevisionResultDto>.CreateNew()
                                                                   .With(x => x.OriginalLanguage = televisionResult.OriginalLanguage)
                                                                   .With(x => x.FirstAirDate = DateTime.Parse(televisionResult.FirstAirDate))
                                                                   .With(x => x.Id = televisionResult.Id)
                                                                   .With(x => x.Name = televisionResult.Name)
                                                                   .With(x => x.OriginalName = televisionResult.OriginalName)
                                                                   .With(x => x.Overview = televisionResult.Overview)
                                                                   .With(x => x.Popularity = televisionResult.Popularity)
                                                                   .With(x => x.VoteAverage = televisionResult.VoteAverage)
                                                                   .With(x => x.VoteCount = televisionResult.VoteCount).Build();

            var actualResult = _mapper.Map<TelevisionResult, TelevisionResultDto>(televisionResult);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }


        
        [Test]
        public void DefaultMappingProfile_When_MovieSearchResponse_Returns_MovieSearchResponseDTO()
        {
            var currentDateTime = DateTime.Now;
            var zeroSecondDate = currentDateTime.AddSeconds(-currentDateTime.Second);

            var movieResults = Builder<MovieResult>.CreateListOfSize(5).All()
                .With(x => x.FirstAirDate = zeroSecondDate.ToString(CultureInfo.InvariantCulture)).Build().ToList();
            var movieSearchResponse = Builder<MovieSearchResponse>.CreateNew()
                                                                                  .With(x => x.Results = movieResults).Build();

            var movieResultsDto = movieResults.Select(movieResult => new MovieResultDto
            {
                FirstAirDate = DateTime.Parse(movieResult.FirstAirDate),
                Id = movieResult.Id,
                Title = movieResult.Title,
                OriginalLanguage = movieResult.OriginalLanguage,
                OriginalTitle = movieResult.OriginalTitle,
                Overview = movieResult.Overview,
                Popularity = movieResult.Popularity,
                VoteAverage = movieResult.VoteAverage,
                VoteCount = movieResult.VoteCount
            }).ToList();

            var expectedResult = Builder<MovieSearchResponseDto>.CreateNew()
                                                                           .With(x => x.Page = movieSearchResponse.Page)
                                                                           .With(x => x.TotalPages = movieSearchResponse.TotalPages)
                                                                           .With(x => x.TotalResults = movieSearchResponse.TotalResults)
                                                                           .With(x => x.Results = movieResultsDto).Build();

            var actualResult = _mapper.Map<MovieSearchResponse, MovieSearchResponseDto>(movieSearchResponse);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_MovieResult_Returns_MovieResultDTO()
        {
            var movieResult = Builder<MovieResult>.CreateNew().With(x => x.FirstAirDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)).Build();
            var expectedResult = Builder<MovieResultDto>.CreateNew()
                                                                   .With(x => x.OriginalLanguage = movieResult.OriginalLanguage)
                                                                   .With(x => x.FirstAirDate = DateTime.Parse(movieResult.FirstAirDate))
                                                                   .With(x => x.Id = movieResult.Id)
                                                                   .With(x => x.Title = movieResult.Title)
                                                                   .With(x => x.OriginalTitle = movieResult.OriginalTitle)
                                                                   .With(x => x.Overview = movieResult.Overview)
                                                                   .With(x => x.Popularity = movieResult.Popularity)
                                                                   .With(x => x.VoteAverage = movieResult.VoteAverage)
                                                                   .With(x => x.VoteCount = movieResult.VoteCount).Build();

            var actualResult = _mapper.Map<MovieResult, MovieResultDto>(movieResult);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

    }
}
