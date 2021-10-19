using Microsoft.Extensions.DependencyInjection;
using CoreServices;

namespace HanSoloService
{
	class Program
	{
		static void Main(string[] args)
		{
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILocationService, LocationService>()
                .AddSingleton<IMessageService, MessageService>()
                .BuildServiceProvider();

           var locationService = serviceProvider.GetService<LocationService>();
           var messageService = serviceProvider.GetService<MessageService>();
        }
	}
}
