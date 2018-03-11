using Microsoft.AspNet.SignalR.Client;
using Poker.WebSocketsClient.Enums;
using System;
using System.Threading.Tasks;

namespace Poker.WebSocketsClient
{
    public static class WebSocketClient
    {
        private static string _serverAddress = "http://raspisnoypoker.gear.host/hubs";
        private static IHubProxy _hubProxy;
        private static bool _isStarted;

        public static bool IsConnected {get; private set; }

        public static async Task Start()
        {
            if (_isStarted)
            {
                return;
            }

            HubConnection hubConnection = new HubConnection(_serverAddress, useDefaultUrl: false);
            _hubProxy = hubConnection.CreateHubProxy("PokerHub");
            try
            {
                await hubConnection.Start();
                _isStarted = true;
            }
            catch (Exception ex)
            {
                var a = ex;
                throw;
            }
        }

        public static async Task ConnectToServerAsync(string userName)
        {
            if (!IsConnected)
            {
                await _hubProxy.Invoke("Connect", userName);
                IsConnected = true;
            }
        }

        public static void AddCallBack<T>(ServerMethod method, Action<T> callback)
        {
            _hubProxy.On<T>($"{method}", (response) =>
            {
                callback(response);
            });
        }

        public static async Task InvokeAsync<T>(ServerMethod method, T argument)
        {
            await _hubProxy.Invoke($"{method}", argument);
        }
    }
}
