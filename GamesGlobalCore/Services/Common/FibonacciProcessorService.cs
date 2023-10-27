using GamesGlobalCore.Constants;
using GamesGlobalCore.Models;
using GamesGlobalCore.Models.DTO;
using GamesGlobalCore.Services.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Services.Common
{
    public class FibonacciProcessorService : IFibonacciProcessorService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        private const string CacheKey = "FibonacciSequence";

        public FibonacciProcessorService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<(string result, List<int>? sequence)> ProcessSequence(List<int> sequence, DateTime expiryDuration, FibonacciDTO fibonacciDTO)
        {
            if (fibonacciDTO.LastNumberIndex == 0 || fibonacciDTO.LastNumberIndex == 1)
            {
                return (FibonacciServiceConstants.SubsequenceLastNumberIndexExist, null);
            }

            //check if expiry duration has elapsed
            if (expiryDuration < DateTime.UtcNow)
            {
                if (sequence.Count <= fibonacciDTO.FirstNumberIndex)
                {
                    return (FibonacciServiceConstants.SubsequenceFirstNumberFailed, null);
                }
                else
                {
                    return (FibonacciServiceConstants.TimeoutOccurred, sequence);
                }
            }

            //check if memory for the subsequence numbers is exhausted
            if (fibonacciDTO.LastNumberIndex >= sequence.Count)
            {
                if (fibonacciDTO.AllocatedMemory <= sequence.Count)
                {
                    return (FibonacciServiceConstants.AllocatedMemoryReached, sequence);
                }
            }

            if ((fibonacciDTO.AllocatedMemory + 1) > sequence.Count)
            {
                int nextItem = sequence[sequence.Count - 2] + sequence[sequence.Count - 1];
                sequence.Add(nextItem);

                return await ProcessSequence(sequence, expiryDuration, fibonacciDTO);
            }

            return (FibonacciServiceConstants.SubsequenceNumbersCreationCompleted, sequence);
        }

        public List<int> GetSubsequence(List<int> sequence, int firstNumberIndex, int lastNumberIndex, int allocatedMemory)
        {
            int counter = 0;

            if (lastNumberIndex > allocatedMemory)
            {
                counter = allocatedMemory - firstNumberIndex;
            }
            else
            {
                counter = lastNumberIndex - firstNumberIndex;
            }

            List<int> subsequence = sequence.GetRange(firstNumberIndex, counter);

            CacheConfig cacheConfig = new();
            _configuration.GetSection(CacheConfig.ConfigName).Bind(cacheConfig);

            if (cacheConfig.InactiveExpiryDuration != 0)
            {
                //store most recent sequence in cache
                MemoryCacheEntryOptions? cacheEntryOptions = new MemoryCacheEntryOptions()
                                                                    .SetSlidingExpiration(TimeSpan.FromMinutes(cacheConfig.InactiveExpiryDuration));

                _cache.Set(CacheKey, sequence, cacheEntryOptions);
            }

            return subsequence;
        }

        public List<int> GetCachedSequence()
        {
            List<int> sequence = null!;

            if (_cache.TryGetValue(CacheKey, out List<int> cachedSequence))
            {
                sequence = cachedSequence;
            }

            return sequence;
        }

    }
}
