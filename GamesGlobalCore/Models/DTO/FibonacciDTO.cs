using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Models.DTO
{
    public class FibonacciDTO
    {
        [Required]
        public int FirstNumberIndex { get; set; }

        [Required]
        public int LastNumberIndex { get; set; }

        public bool ShouldCacheBeUsed { get; set; }

        [Required]
        public int ProcessingTime { get; set; }     //in milliseconds

        [Required]
        public int AllocatedMemory { get; set; }
    }

    public class FibonacciSubsequenceDTO
    {
        public List<int>? Subsequence { get; set; }
    }
}
