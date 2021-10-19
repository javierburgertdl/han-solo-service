using CoreServices;
using System;
using System.Collections.Generic;
using Xunit;

namespace HanSoloService.Test
{
	public class LocationServiceTest
	{
		[Fact]
		public void GetLocation_NullParameters_ShouldReturnNullMessage()
		{
			var locationService = new LocationService();

			var ex = Assert.Throws<Exception>(() => locationService.GetLocation(null));

			Assert.Equal("Trilateration algorithm needs 3 reference points.", ex.Message);
		}

		[Fact]
		public void GetLocation_EmptyParameters_ShouldReturnNull()
		{
			var locationService = new LocationService();

			var satellite1 = new Satellite();
			var satellite2 = new Satellite();
			var satellites = new List<Satellite> { satellite1, satellite2};
			
			var ex = Assert.Throws<Exception>(() => locationService.GetLocation(satellites));

			Assert.Equal("Trilateration algorithm needs 3 reference points.", ex.Message);
		}

		[Theory]
		[InlineData(-300, -400, 500, 400, 300, 500, -400, 300, 500, 0, 0 )]
		[InlineData(500, 600, 500, -100, -200, 500, -200, -100, 500, 200, 200)]
		[InlineData(-500, -200, 700, 100, -100, 848.52, 500, 100, 1077.03, -499.98, 499.94)]
		public void GetLocation_RightParameters_ShouldReturnRightMessage(double sat1posX, double sat1posY, double distance1, double sat2posX, double sat2posY, double distance2, double sat3posX, double sat3posY, double distance3, double expectedResultX, double expectedResultY)
		{
			var locationService = new LocationService();

			var satellite1 = new Satellite { Name = "kenobi", Distance = distance1, Position = new Position { X = sat1posX, Y = sat1posY }  };
			var satellite2 = new Satellite { Name = "skywalker", Distance = distance2, Position = new Position { X = sat2posX, Y = sat2posY } };
			var satellite3 = new Satellite { Name = "sato", Distance = distance3, Position = new Position { X = sat3posX, Y = sat3posY } };

			var satellites = new List<Satellite> { satellite1, satellite2, satellite3 };

			var result = locationService.GetLocation(satellites);

			Assert.Equal(result.X, expectedResultX);
		}


	}
}
