using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcs.Application.Server
{
    public class ContextHolder : IContextHolder
    {
        public IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
        public IHubCallerConnectionContext<dynamic> BlotterHubClients { get; set; }
        public IHubCallerConnectionContext<dynamic> ReferenceDataHubClients { get; set; }
    }
}
