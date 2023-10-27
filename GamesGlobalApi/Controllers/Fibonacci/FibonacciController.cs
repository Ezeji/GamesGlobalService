using GamesGlobalCore.Models.DTO;
using GamesGlobalCore.Services;
using GamesGlobalCore.Services.Fibonacci.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace GamesGlobalApi.Controllers.Fibonacci
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _service;

        public FibonacciController(IFibonacciService service)
        {
            _service = service;
        }

        [HttpPost("subsequence")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> PostSubsequence([FromBody] FibonacciDTO fibonacciDTO)
        {
            await Task.Delay(500);

            ServiceResponse<FibonacciSubsequenceDTO> response = await _service.CreateSubsequenceAsync(fibonacciDTO);

            return response.FormatResponse();
        }
    }
}
