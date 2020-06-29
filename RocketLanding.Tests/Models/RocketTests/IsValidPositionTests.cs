using FluentAssertions;
using RocketLanding.Models;
using System.Collections.Generic;
using Xunit;

namespace RocketLanding.Tests.Models.RocketTests
{
    public class IsValidPositionTests
    {
        private readonly Rocket _sut;
        private readonly LandingPlatform _landingPlatform;

        public IsValidPositionTests()
        {
            _landingPlatform =
                new LandingPlatform
                {
                    Position =
                        new Position
                        {
                            X = 5,
                            Y = 5
                        },
                    Size =
                        new Size
                        {
                            Height = 10,
                            Width = 10
                        }
                };
            _sut = new Rocket();
        }

        [Fact]
        public void
            Given_A_Rocket_And_A_Landing_Platform_With_10x10_Size_And_Position_5_5_When_Asking_Valid_Position_Should_Return_Ok_For_Landing()
        {
            var position =
                new Position
                {
                    X = 5,
                    Y = 5
                };
            var result = _sut.IsValidPosition(position, _landingPlatform);
            result.Should().Be("ok for landing");
        }

        [Fact]
        public void
            Given_A_Rocket_And_A_Landing_Platform_With_10x10_Size_And_Position_5_5_When_Asking_Invalid_Position_Should_Return_Out_Of_Platform()
        {
            var position =
                new Position
                {
                    X = 15,
                    Y = 16
                };
            var result = _sut.IsValidPosition(position, _landingPlatform);
            result.Should().Be("out of platform");
        }

        [Fact]
        public void Given_A_Rocket_And_A_Landing_Platform_With_10x10_Size_And_Position_2_2_When_Asking_For_Previous_Checked_Position_Should_Return_Clash()
        {
            var rocket =
                new Rocket
                {
                    Position =
                        new Position
                        {
                            X = 10,
                            Y = 4
                        }
                };

            var rockets =
                new List<Rocket>
                {
                    rocket
                };

            var landingPlatform =
                new LandingPlatform
                {
                    Position = new Position
                    {
                        X = 2,
                        Y = 2
                    },
                    Size = new Size
                    {
                        Height = 10,
                        Width = 10
                    },
                    Rockets = rockets
                };

            var position =
                new Position
                {
                    X = 9,
                    Y = 4
                };
            var result = _sut.IsValidPosition(position, landingPlatform);
            result.Should().Be("clash");
        }

        [Theory]
        [InlineData(9, 3)]
        [InlineData(9, 4)]
        [InlineData(9, 5)]
        [InlineData(10, 3)]
        [InlineData(10, 5)]
        [InlineData(11, 3)]
        [InlineData(11, 4)]
        [InlineData(11, 5)]
        public void Given_A_Rocket_And_A_Landing_Platform_With_10x10_Size_And_Position_2_2_When_Asking_For_Next_To_Previous_Checked_Position(
                int x, int y)
        {
            var rocket =
                new Rocket
                {
                    Position =
                        new Position
                        {
                            X = 10,
                            Y = 4
                        }
                };

            var rockets =
                new List<Rocket>
                {
                    rocket
                };

            var landingPlatform =
                new LandingPlatform
                {
                    Position =
                        new Position
                        {
                            X = 2,
                            Y = 2
                        },
                    Size =
                        new Size
                        {
                            Height = 10,
                            Width = 10
                        },
                    Rockets = rockets
                };

            var position =
                new Position
                {
                    X = x,
                    Y = y
                };
            var result = _sut.IsValidPosition(position, landingPlatform);
            result.Should().Be("clash");
        }

        [Theory]
        [InlineData(10, 10, "clash")]
        [InlineData(10, 11, "clash")]
        [InlineData(10, 12, "out of platform")]
        [InlineData(11, 10, "clash")]
        [InlineData(11, 12, "out of platform")]
        [InlineData(12, 10, "out of platform")]
        [InlineData(12, 11, "out of platform")]
        [InlineData(12, 12, "out of platform")]
        public void Given_A_Rocket_And_A_Landing_Platform_With_10x10_Size_And_Position_1_1_When_Asking_For_Next_To_Previous_Checked_Position_And_Out_Of_Platform(
                int x, int y, string expectedResult)
        {
            var rocket =
                new Rocket
                {
                    Position =
                        new Position
                        {
                            X = 11,
                            Y = 11
                        }
                };

            var rockets =
                new List<Rocket>
                {
                    rocket
                };

            var landingPlatform =
                new LandingPlatform
                {
                    Position =
                        new Position
                        {
                            X = 1,
                            Y = 1
                        },
                    Size =
                        new Size
                        {
                            Height = 10,
                            Width = 10
                        },
                    Rockets = rockets
                };

            var position =
                new Position
                {
                    X = x,
                    Y = y
                };
            var result = _sut.IsValidPosition(position, landingPlatform);
            result.Should().Be(expectedResult);
        }
    }
}