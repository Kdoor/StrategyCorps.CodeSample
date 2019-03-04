using System.Linq;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using NUnit.Framework;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.MappingProfiles;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.Tests.MappingProfiles
{
    [TestFixture]
    public class DefaultMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<DefaultMappingProfile>(); });
            _mapper = config.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            _mapper = null;
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionSearchResponseDTO_Returns_TelevisionSearchResponseViewModel()
        {
            var televisionResultsDto = Builder<TelevisionResultDto>.CreateListOfSize(5).Build().ToList();
            var televisionSearchResponseDto = Builder<TelevisionSearchResponseDto>.CreateNew()
                .With(x => x.Results = televisionResultsDto).Build();

            var televisionResultsViewModel = televisionResultsDto.Select(televisionResultDto => new TelevisionResultViewModel
            {
                FirstAirDate = televisionResultDto.FirstAirDate.ToString(),
                Id = televisionResultDto.Id,
                Name = televisionResultDto.Name,
                OriginalLanguage = televisionResultDto.OriginalLanguage,
                OriginalName = televisionResultDto.OriginalName,
                Overview = televisionResultDto.Overview,
                Popularity = televisionResultDto.Popularity,
                VoteAverage = televisionResultDto.VoteAverage,
                VoteCount = televisionResultDto.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseViewModel>.CreateNew()
                .With(x => x.Page = televisionSearchResponseDto.Page)
                .With(x => x.TotalPages = televisionSearchResponseDto.TotalPages)
                .With(x => x.TotalResults = televisionSearchResponseDto.TotalResults)
                .With(x => x.Results = televisionResultsViewModel).Build();

            var actualResult = _mapper.Map<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>(televisionSearchResponseDto);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionResultDTO_Returns_TelevisionResultViewModel()
        {
            var televisionResultDto = Builder<TelevisionResultDto>.CreateNew().Build();
            var expectedResult = Builder<TelevisionResultViewModel>.CreateNew()
                .With(x => x.OriginalLanguage = televisionResultDto.OriginalLanguage)
                .With(x => x.FirstAirDate = televisionResultDto.FirstAirDate.ToString())
                .With(x => x.Id = televisionResultDto.Id)
                .With(x => x.Name = televisionResultDto.Name)
                .With(x => x.OriginalName = televisionResultDto.OriginalName)
                .With(x => x.Overview = televisionResultDto.Overview)
                .With(x => x.Popularity = televisionResultDto.Popularity)
                .With(x => x.VoteAverage = televisionResultDto.VoteAverage)
                .With(x => x.VoteCount = televisionResultDto.VoteCount).Build();

            var actualResult = _mapper.Map<TelevisionResultDto, TelevisionResultViewModel>(televisionResultDto);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }


        [Test]
        public void DefaultMappingProfile_When_MovieSearchResponseDTO_Returns_MovieSearchResponseViewModel()
        {
            var movieResultsDto = Builder<MovieResultDto>.CreateListOfSize(5).Build().ToList();
            var movieSearchResponseDto = Builder<MovieSearchResponseDto>.CreateNew()
                .With(x => x.Results = movieResultsDto).Build();

            var movieResultsViewModel = movieResultsDto.Select(movieResultDto => new MovieResultViewModel
            {
                FirstAirDate = movieResultDto.FirstAirDate.ToString(),
                Id = movieResultDto.Id,
                Title = movieResultDto.Title,
                OriginalLanguage = movieResultDto.OriginalLanguage,
                OriginalTitle = movieResultDto.OriginalTitle,
                Overview = movieResultDto.Overview,
                Popularity = movieResultDto.Popularity,
                VoteAverage = movieResultDto.VoteAverage,
                VoteCount = movieResultDto.VoteCount
            }).ToList();

            var expectedResult = Builder<MovieSearchResponseViewModel>.CreateNew()
                .With(x => x.Page = movieSearchResponseDto.Page)
                .With(x => x.TotalPages = movieSearchResponseDto.TotalPages)
                .With(x => x.TotalResults = movieSearchResponseDto.TotalResults)
                .With(x => x.Results = movieResultsViewModel).Build();

            var actualResult = _mapper.Map<MovieSearchResponseDto, MovieSearchResponseViewModel>(movieSearchResponseDto);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_MovieResultDTO_Returns_MovieResultViewModel()
        {
            var movieResultDto = Builder<MovieResultDto>.CreateNew().Build();
            var expectedResult = Builder<MovieResultViewModel>.CreateNew()
                .With(x => x.OriginalLanguage = movieResultDto.OriginalLanguage)
                .With(x => x.FirstAirDate = movieResultDto.FirstAirDate.ToString())
                .With(x => x.Id = movieResultDto.Id)
                .With(x => x.Title = movieResultDto.Title)
                .With(x => x.OriginalTitle = movieResultDto.OriginalTitle)
                .With(x => x.Overview = movieResultDto.Overview)
                .With(x => x.Popularity = movieResultDto.Popularity)
                .With(x => x.VoteAverage = movieResultDto.VoteAverage)
                .With(x => x.VoteCount = movieResultDto.VoteCount).Build();

            var actualResult = _mapper.Map<MovieResultDto, MovieResultViewModel>(movieResultDto);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}
