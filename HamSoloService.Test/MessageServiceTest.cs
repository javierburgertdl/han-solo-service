using CoreServices;
using System;
using System.Collections.Generic;
using Xunit;

namespace HanSoloService.Test
{
	public class MessageServiceTest
	{
		[Fact]
		public void GetMessage_NullParameters_ShouldThrowException()
		{
			var messageService = new MessageService();

			var ex = Assert.Throws<Exception>(() => messageService.GetMessage(null));

			Assert.Equal("Service needs 1 satellite at least to get the message.", ex.Message);
		}

		[Fact]
		public void GetMessage_EmptyParameters_ShouldThrowException()
		{
			var messageService = new MessageService();

			var satellites = new List<Satellite>();

			var ex = Assert.Throws<Exception>(() => messageService.GetMessage(satellites));

			Assert.Equal("Service needs 1 satellite at least to get the message.", ex.Message);
		}

		[Fact]
		public void GetMessage_EmptyMessages_ShouldThrowException()
		{
			var messageService = new MessageService();

			var satellite1 = new Satellite();
			var satellite2 = new Satellite();
			var satellites = new List<Satellite> { satellite1, satellite2 };

			var ex = Assert.Throws<Exception>(() => messageService.GetMessage(satellites));

			Assert.Equal("Messages cannot be null.", ex.Message);
		}

		[Theory]
		[InlineData(new string[] { "", "este", "", "un", "mensaje" }, new string[] { "" ,"este", "", "", "mensaje" }, new string[] { "", "es", "", "mensaje" }, "este es un mensaje")]
		[InlineData(new string[] { "", "", "otro", "", "", "prueba" }, new string[] { "", "", "", "", "mensaje", "de", "prueba" }, new string[] { "", "mensaje", "de", "" }, "otro mensaje de prueba")]
		public void GetMessage_RightParameters_ShouldReturnRightMessage(string[] message1, string[] message2, string[] message3, string expectedResult)
		{
			var messageService = new MessageService();

			var satellite1 = new Satellite { Name = "kenobi", Message = message1 };
			var satellite2 = new Satellite { Name = "skywalker", Message = message2 };
			var satellite3 = new Satellite { Name = "sato", Message = message3 };
			var satellites = new List<Satellite> { satellite1, satellite2, satellite3 };
			var result = messageService.GetMessage(satellites);

			Assert.Equal(result, expectedResult);
		}
	}
}
