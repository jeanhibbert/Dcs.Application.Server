using System;

namespace Dcs.Application.Client
{
    using Microsoft.AspNet.SignalR.Client;

    class Connection : IDisposable
    {
        private readonly HubConnection _hubConnection;

        public Connection()
        {
            _hubConnection = new HubConnection(@"http://localhost:8080");
            _hubConnection.Headers.Add("User", "testuser");
            PricingHubProxy = _hubConnection.CreateHubProxy("PricingHub");
            _hubConnection.Start().Wait();
        }

        public IHubProxy PricingHubProxy { get; set; }

        public void Dispose()
        {
        }
    }
}
