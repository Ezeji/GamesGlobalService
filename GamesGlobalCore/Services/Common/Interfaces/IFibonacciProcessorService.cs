using GamesGlobalCore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Services.Common.Interfaces
{
    public interface IFibonacciProcessorService
    {
        Task<(string result, List<int>? sequence)> ProcessSequence(List<int> sequence, DateTime expiryDuration, FibonacciDTO fibonacciDTO);
        List<int> GetSubsequence(List<int> sequence, int firstNumberIndex, int lastNumberIndex, int allocatedMemory);
        List<int> GetCachedSequence();
    }
}
