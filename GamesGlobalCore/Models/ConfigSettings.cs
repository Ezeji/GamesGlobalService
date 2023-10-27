using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Models
{
    public class CacheConfig
    {
        public const string ConfigName = nameof(CacheConfig);

        public int InactiveExpiryDuration { get; set; }
    }
}
