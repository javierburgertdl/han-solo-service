using System.Collections.Generic;

namespace CoreServices
{
	public interface IMessageService
	{
		string GetMessage(List<Satellite> satellites);
	}
}
