using Microsoft.AspNet.SignalR;

namespace Poker.Server.Hubs
{
    public class PokerHub : Hub
    {
        public void Connect(string name)
        {
            Clients.All.Connect($"Hub says: {name} just connected");
            
        }

        public void SendMessage(string message)
        {
            Clients.All.SendMessage($"{Context.ConnectionId} says: {message}");
        }
    }
}