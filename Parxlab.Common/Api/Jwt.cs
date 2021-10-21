using System;

namespace Parxlab.Common.Api
{
    public class Jwt
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime{get; set; }
    }
}