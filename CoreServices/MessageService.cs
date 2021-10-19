using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreServices
{
	public class MessageService : IMessageService
	{
		//public string GetMessage(Satellite satellite1, Satellite satellite2, Satellite satellite3)
		//{
		//	int messageLenght = Math.Min(satellite1.Message.Length, Math.Min(satellite2.Message.Length, satellite3.Message.Length));
		//	string[] result = new string[messageLenght];
		//	int cont = 0;
		//	int offset1 = satellite1.Message.Length - messageLenght;
		//	int offset2 = satellite2.Message.Length - messageLenght;
		//	int offset3 = satellite3.Message.Length - messageLenght;
		//	while (cont < messageLenght)
		//	{
		//		result[cont] = getWord(satellite1.Message[cont+offset1], satellite2.Message[cont+offset2], satellite3.Message[cont+offset3]);
		//		cont++;
		//	}
		//	return string.Join(" ", result);
		//}

		public string GetMessage(List<Satellite> satellites)
		{
			if (satellites == null || satellites.Count == 0)
			{
				throw new Exception("Service needs 1 satellite at least to get the message.");
			}

			if (satellites.Exists(s => s.Message == null))
			{
				throw new Exception("Messages cannot be null.");
			}

			int minMessageLenght = satellites.Min(s => s.Message.Length);
			//int messageLenght = Math.Min(satellite1.Message.Length, Math.Min(satellite2.Message.Length, satellite3.Message.Length));
			var satellitesOffset = new int[satellites.Count];
			
			for (int i = 0; i < satellites.Count; i++)
			{
				satellitesOffset[i] = satellites[i].Message.Length - minMessageLenght;
			}
			
			string[] result = new string[minMessageLenght];
			for (int i = 0; i < minMessageLenght; i++)
			{

				string word = string.Empty;
				bool found = false;
				for (int j = 0; j < satellites.Count && !found; j++)
				{
					if(!string.IsNullOrEmpty(satellites[j].Message[i + satellitesOffset[j]]))
					{
						word = satellites[j].Message[i + satellitesOffset[j]];
						found = true;
					}
				}
				result[i] = word;
			}
			return string.Join(" ", result);
		}
	}
}
