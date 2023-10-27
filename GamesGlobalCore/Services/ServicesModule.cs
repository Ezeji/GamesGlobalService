using GamesGlobalCore.Services.Common;
using GamesGlobalCore.Services.Common.Interfaces;
using GamesGlobalCore.Services.Fibonacci;
using GamesGlobalCore.Services.Fibonacci.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Services
{
    public static class ServicesModule
    {
        public static void AddServices(this IServiceCollection services)
        {
            //Common
            services.AddScoped<IFibonacciProcessorService, FibonacciProcessorService>();

            //Fibonacci
            services.AddScoped<IFibonacciService, FibonacciService>();
        }
    }
}
