using GamesGlobalCore.Constants;
using GamesGlobalCore.Models.DTO;
using GamesGlobalCore.Services;
using GamesGlobalCore.Services.Common.Interfaces;
using GamesGlobalCore.Services.Fibonacci;
using GamesGlobalCore.Services.Fibonacci.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GamesGlobalTests.ServicesTests
{
    public class FibonacciServiceTest
    {
        private readonly Mock<IFibonacciProcessorService> processorServiceMock = new Mock<IFibonacciProcessorService>(MockBehavior.Strict);

        private IFibonacciService service;

        public FibonacciServiceTest()
        {
        }

        [Fact]
        public async Task CreateSubsequenceAsync_Should_Return_ParameterEmptyOrNull_If_FibonacciDTO_IsNull()
        {
            //Arrange
            FibonacciDTO fibonacciDTO = null!;

            service = new FibonacciService(processorServiceMock.Object);

            string expected = ServiceMessages.ParameterEmptyOrNull;

            //Act
            ServiceResponse<FibonacciSubsequenceDTO> actual = await service.CreateSubsequenceAsync(fibonacciDTO);

            //Assert
            Assert.Equal(expected, actual.StatusMessage);
        }

        [Fact]
        public async Task CreateSubsequenceAsync_Should_Return_NullData_If_FibonacciDTO_IsNotNull_And_Sequence_Is_Null()
        {
            //Arrange
            FibonacciDTO fibonacciDTO = new()
            {
                ShouldCacheBeUsed = true
            };

            List<int> cachedSequence = new();

            processorServiceMock.Setup(x => x.GetCachedSequence()).Returns(cachedSequence);

            (string result, List<int>? sequence) sequenceResponse = (string.Empty, null);

            processorServiceMock.Setup(x => x.ProcessSequence(It.IsAny<List<int>>(), It.IsAny<DateTime>(), It.IsAny<FibonacciDTO>())).ReturnsAsync(sequenceResponse);

            service = new FibonacciService(processorServiceMock.Object);

            //Act
            ServiceResponse<FibonacciSubsequenceDTO> actual = await service.CreateSubsequenceAsync(fibonacciDTO);

            //Assert
            Assert.Null(actual.ResponseObject);
        }

        [Fact]
        public async Task CreateSubsequenceAsync_Should_Return_SubsequenceData_If_FibonacciDTO_IsNotNull_And_Sequence_IsNotNull()
        {
            //Arrange
            FibonacciDTO fibonacciDTO = new();

            List<int> cachedSequence = new();

            processorServiceMock.Setup(x => x.GetCachedSequence()).Returns(cachedSequence);

            (string result, List<int>? sequence) sequenceResponse = (string.Empty, new List<int>());

            processorServiceMock.Setup(x => x.ProcessSequence(It.IsAny<List<int>>(), It.IsAny<DateTime>(), It.IsAny<FibonacciDTO>())).ReturnsAsync(sequenceResponse);

            List<int> subsequence = new();

            processorServiceMock.Setup(x => x.GetSubsequence(It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(subsequence);

            service = new FibonacciService(processorServiceMock.Object);

            //Act
            ServiceResponse<FibonacciSubsequenceDTO> actual = await service.CreateSubsequenceAsync(fibonacciDTO);

            //Assert
            Assert.NotNull(actual.ResponseObject);
        }
    }
}
