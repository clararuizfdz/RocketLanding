using System.Collections.Generic;

namespace RocketLanding.Models
{
    public class LandingPlatform
    {
        public Size Size { get; set; }
        public Position Position { get; set; }
        public IEnumerable<Rocket> Rockets { get; set; }
    }
}
