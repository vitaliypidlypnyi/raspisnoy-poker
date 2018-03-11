using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Poker.ConsoleClient
{
    class Program
    {
        private static IHubProxy _stockTickerHubProxy;

        static void Main(string[] args)
        {
            Task.Run(() => ConfigureHubProxy()).GetAwaiter().GetResult();
            Console.WriteLine("Enter Name");
            var name = Console.ReadLine();
            Task.Run(() => Connect(name)).GetAwaiter().GetResult();

            while (true)
            {
                var message = Console.ReadLine();
                Task.Run(() => SendMessage(message)).GetAwaiter().GetResult();
            }

        }

        private static async Task ConfigureHubProxy()
        {
            //HubConnection hubConnection = new HubConnection("http://raspisnoypoker.gear.host/hubs", useDefaultUrl: false);
            HubConnection hubConnection = new HubConnection("http://localhost:1234/hubs", useDefaultUrl: false);

            _stockTickerHubProxy = hubConnection.CreateHubProxy("PokerHub");
            await hubConnection.Start();

            _stockTickerHubProxy.On<string>("Connect", message =>
            {
                Console.WriteLine(message);
            });

            _stockTickerHubProxy.On<string>("SendMessage", message =>
            {
                Console.WriteLine(message);
            });
        }

        private static async Task Connect(string userName)
        {
            
            await _stockTickerHubProxy.Invoke("Connect", userName);
        }

        private static async Task SendMessage(string message)
        {
            await _stockTickerHubProxy.Invoke("SendMessage", message);
        }
    }
}
