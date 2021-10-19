using System.Collections.Generic;

namespace CoreServices
{
	public interface ILocationService
	{
		//Position GetLocation(Satellite satellite1, Satellite satellite2, Satellite satellite3);
		Position GetLocation(List<Satellite> satellites);
	}
}
