using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreServices
{
	public class LocationService : ILocationService
	{
		const int SATELLITE_TRILATERATION_NUMBER = 3;

		public Position GetLocation(List<Satellite> satellites)
		{
			if(satellites == null || satellites.Count != SATELLITE_TRILATERATION_NUMBER || satellites.Exists(s => s == null))
			{
				throw new Exception("Trilateration algorithm needs 3 reference points.");
			}

			//This is calculated using a trilateration algotithm. For any reference, please visit 
			//https://www.101computing.net/cell-phone-trilateration-algorithm/

			var A = (-2 * satellites[0].Position.X + 2 * satellites[1].Position.X);
			var B = (-2 * satellites[0].Position.Y + 2 * satellites[1].Position.Y);
			var C = Math.Pow(satellites[0].Distance, 2) - Math.Pow(satellites[1].Distance, 2) - Math.Pow(satellites[0].Position.X, 2) + Math.Pow(satellites[1].Position.X, 2) - Math.Pow(satellites[0].Position.Y, 2) + Math.Pow(satellites[1].Position.Y, 2);

			var D = (-2 * satellites[1].Position.X + 2 * satellites[2].Position.X);
			var E = (-2 * satellites[1].Position.Y + 2 * satellites[2].Position.Y);
			var F = Math.Pow(satellites[1].Distance, 2) - Math.Pow(satellites[2].Distance, 2) - Math.Pow(satellites[1].Position.X, 2) + Math.Pow(satellites[2].Position.X, 2) - Math.Pow(satellites[1].Position.Y, 2) + Math.Pow(satellites[2].Position.Y, 2);
			var X = ((C * E) - (F * B)) / ((E * A) - (B * D));
			var Y = ((C * D) - (A * F)) / ((B * D) - (A * E));
			var result = new Position { X = Math.Round(X, 2, MidpointRounding.AwayFromZero), Y = Math.Round(Y, 2, MidpointRounding.AwayFromZero) };

			return result;
		}
	}
}
