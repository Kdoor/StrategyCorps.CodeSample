using System;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.Controllers;
using StrategyCorps.CodeSample.WebApi.Tests.Extensions;
using StrategyCorps.CodeSample.WebApi.ViewModels;
using ILogger = NLog.ILogger;

namespace StrategyCorps.CodeSample.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MovieSearchByQueryTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void MovieSearchByQuery_When_QueryIsNullOrWhitespace_Returns_BadRequest(string query)
        {
            var movieController = new MovieController(null, null, null);
            var actionResult = movieController.MovieSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        [TestCase("Monsters Inc")]
        public void MovieSearchByQuery_When_MovieServiceReturnsNull_Returns_NotFound(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var movieServiceMock = new Mock<IMovieService>();
            movieServiceMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Returns((MovieSearchResponseDto) null);

            var movieController = new MovieController(movieServiceMock.Object, logger.Object, null);
            var actionResult = movieController.MovieSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        [TestCase("Monsters Inc")]
        public void MovieSearchByQuery_When_MovieServiceThrowsException_Returns_InternalServerError(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var movieServiceMock = new Mock<IMovieService>();
            movieServiceMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Throws<Exception>();

            var movieController = new MovieController(movieServiceMock.Object, logger.Object, null);
            var actionResult = movieController.MovieSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        [TestCase("Monsters Inc")]
        public void MovieSearchByQuery_When_MovieServiceThrowsStrategyCorpsException_Returns_InternalServerError(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var movieServiceMock = new Mock<IMovieService>();
            movieServiceMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Throws<StrategyCorpsException>();

            var movieController = new MovieController(movieServiceMock.Object, logger.Object, null);
            var actionResult = movieController.MovieSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        [TestCase("Monsters Inc")]
        public void MovieSearchByQuery_When_MovieServiceReturnsMovieSearchResponseDTO_Returns_Ok(string query)
        {
            var movieSearchResponseDto = Builder<MovieSearchResponseDto>.CreateNew().Build();
            var movieResultViewModels = Builder<MovieResultViewModel>.CreateListOfSize(5).Build();
            var expectedResult = Builder<MovieSearchResponseViewModel>.CreateNew()
                .With(x => x.Results = movieResultViewModels.ToList()).Build();

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<MovieSearchResponseDto, MovieSearchResponseViewModel>(It.IsAny<MovieSearchResponseDto>()))
                .Returns(expectedResult).Verifiable();

            var movieServiceMock = new Mock<IMovieService>();
            movieServiceMock.Setup(x => x.GetMoviesByQuery(It.IsAny<string>())).Returns(movieSearchResponseDto);

            var movieController = new MovieController(movieServiceMock.Object, loggerMock.Object, mapperMock.Object);
            var actionResult = movieController.MovieSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<OkNegotiatedContentResult<MovieSearchResponseViewModel>>();
            response.Content.ToExpectedObject().ShouldEqual(expectedResult);

            loggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
            mapperMock.Verify(x => x.Map<MovieSearchResponseDto, MovieSearchResponseViewModel>(It.IsAny<MovieSearchResponseDto>()), Times.Once);
        }
    }
}
