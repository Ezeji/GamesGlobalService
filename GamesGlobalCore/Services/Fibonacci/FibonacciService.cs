using GamesGlobalCore.Constants;
using GamesGlobalCore.Models;
using GamesGlobalCore.Models.DTO;
using GamesGlobalCore.Services.Common.Interfaces;
using GamesGlobalCore.Services.Fibonacci.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Services.Fibonacci
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IFibonacciProcessorService _processorService;

        public FibonacciService(IFibonacciProcessorService processorService)
        {
            _processorService = processorService;
        }

        public async Task<ServiceResponse<FibonacciSubsequenceDTO>> CreateSubsequenceAsync(FibonacciDTO fibonacciDTO)
        {
            if (fibonacciDTO == null)
            {
                return ServiceResponse<FibonacciSubsequenceDTO>.Failed(ServiceMessages.ParameterEmptyOrNull);
            }

            List<int> sequence = new()
            {
                0,
                1
            };

            //check if cache is to be used for processing
            //the subsequence numbers generation
            if (fibonacciDTO.ShouldCacheBeUsed)
            {
                List<int> cachedSequence = _processorService.GetCachedSequence();

                if (cachedSequence != null)
                {
                    sequence.Clear();
                    sequence.AddRange(cachedSequence);
                }
            }

            DateTime expiryDuration = DateTime.UtcNow.AddMilliseconds(fibonacciDTO.ProcessingTime);

            //process sequence recursively
            (string result, List<int>? sequence) sequenceResponse = await _processorService.ProcessSequence(sequence, expiryDuration, fibonacciDTO);

            if (sequenceResponse.sequence == null)
            {
                return ServiceResponse<FibonacciSubsequenceDTO>.Failed(sequenceResponse.result);
            }

            //get subsequence from sequence
            List<int> subsequence = _processorService.GetSubsequence(sequenceResponse.sequence, fibonacciDTO.FirstNumberIndex, fibonacciDTO.LastNumberIndex, fibonacciDTO.AllocatedMemory);

            //map to dto
            FibonacciSubsequenceDTO subsequenceDTO = new()
            {
                Subsequence = subsequence
            };

            return ServiceResponse<FibonacciSubsequenceDTO>.Success(subsequenceDTO, sequenceResponse.result);
        }
    }
}
