using GamesGlobalCore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Services.Fibonacci.Interfaces
{
    public interface IFibonacciService
    {
        /// <summary>
		/// Create subsequence.
		/// </summary>
        /// <param name="fibonacciDTO"></param>
		/// <returns>Returns a specific status code if there's an error. Otherwise, returns a Fibonacci Subsequence DTO</returns>
        Task<ServiceResponse<FibonacciSubsequenceDTO>> CreateSubsequenceAsync(FibonacciDTO fibonacciDTO);
    }
}
