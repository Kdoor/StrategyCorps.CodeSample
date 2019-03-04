using System;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.MovieService
{
    [TestFixture]
    public class GetMoviesByQueryTests
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
        public void GetMoviesByQuery_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Throws<Exception>();
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            Assert.Catch<Exception>(() => movieService.GetMoviesByQuery(null));
        }

        [Test]
        public void GetMoviesByQuery_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_StrategyCorpsException()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Throws(expectedException);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            var actualException = Assert.Catch<Exception>(() => movieService.GetMoviesByQuery(null));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetMoviesByQuery_When_Successful_Returns_MovieSearchResponseDTO()
        {
            var expectedResult = Builder<MovieSearchResponseDto>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Returns(expectedResult);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);
            var actualResult = movieService.GetMoviesByQuery(null);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}