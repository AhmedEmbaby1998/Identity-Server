using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.Helpers
{
    public class JwtSettings
    {
        public string Secret { set; get; }
        public TimeSpan TokenLifeTime { set; get; }
    }
}
