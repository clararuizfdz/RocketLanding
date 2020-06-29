using System.Linq;

namespace RocketLanding.Models
{
    public class Rocket
    {
        public Position Position { get; set; }
        
        public string IsValidPosition(Position position, LandingPlatform landingPlatform)
        {
            var isValidX = position.X >= landingPlatform.Position.X &&
                           position.X <= landingPlatform.Position.X + landingPlatform.Size.Width;

            var isValidY = position.Y >= landingPlatform.Position.Y &&
                           position.Y <= landingPlatform.Position.Y + landingPlatform.Size.Height;

            if (isValidX && isValidY)
            {
                if (!(landingPlatform.Rockets is null) && landingPlatform.Rockets.Any())
                {
                    foreach (var rocket in landingPlatform.Rockets)
                    {
                        if (IsNextPosition(rocket.Position, position))
                        {
                            return "clash";
                        }
                    }
                }

                return "ok for landing";
            }
            else
            {
                return "out of platform";
            }
        }

        private bool IsNextPosition(Position previousPosition, Position position)
        {
           
            var isNextPositionX = IsNextAxisPosition(previousPosition.X, position.X);
            var isNextPositionY = IsNextAxisPosition(previousPosition.Y, position.Y);

            return isNextPositionX && isNextPositionY;
        }

        private bool IsNextAxisPosition(int previousPosition, int position)
        {
            return position == previousPosition || position == previousPosition + 1 || position == previousPosition - 1;
        }
    }
}
