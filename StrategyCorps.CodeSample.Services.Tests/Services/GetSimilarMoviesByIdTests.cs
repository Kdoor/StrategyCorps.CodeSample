using System;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.MovieService
{
    [TestFixture]
    public class GetSimilarMoviesByIdTests
    {
        private Mock<IMovieEntertainmentDispatcher> _entertainmentDispatcherMock;

        [SetUp]
        public void SetUp()
        {
            _entertainmentDispatcherMock = new Mock<IMovieEntertainmentDispatcher>();
        }

        [TearDown]
        public void TearDown()
        {
            _entertainmentDispatcherMock = null;
        }

        [Test]
        public void GetSimilarMovieById_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarMoviesById(It.IsAny<int>())).Throws<Exception>();
            var MovieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            Assert.Catch<Exception>(() => MovieService.GetSimilarMoviesById(0));
        }

        [Test]
        public void GetSimilarMovieById_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_Exception()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarMoviesById(It.IsAny<int>())).Throws(expectedException);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            var actualException = Assert.Catch<Exception>(() => movieService.GetSimilarMoviesById(0));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetSimilarMovieById_When_Successful_Returns_MovieSearchResponseDTO()
        {
            var expectedResult = Builder<MovieSearchResponseDto>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarMoviesById(It.IsAny<int>())).Returns(expectedResult);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);
            var actualResult = movieService.GetSimilarMoviesById(0);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}