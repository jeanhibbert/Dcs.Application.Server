using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcs.Application.Server
{
    using Microsoft.AspNet.SignalR.Hubs;

    public interface IContextHolder
    {
        IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
        IHubCallerConnectionContext<dynamic> BlotterHubClients { get; set; }
        IHubCallerConnectionContext<dynamic> ReferenceDataHubClients { get; set; }
    }
}
