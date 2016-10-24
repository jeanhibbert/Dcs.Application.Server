using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            //this._hubConnection
        }

        public IHubProxy PricingHubProxy { get; set; }

        public void Dispose()
        {
        }
    }
}
