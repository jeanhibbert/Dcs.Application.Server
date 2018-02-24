using Microsoft.AspNet.SignalR.Hubs;

namespace Dcs.Application.Server
{
    public class ContextHolder : IContextHolder
    {
        public IHubCallerConnectionContext<dynamic> PricingHubClient { get; set; }
        public IHubCallerConnectionContext<dynamic> BlotterHubClients { get; set; }
        public IHubCallerConnectionContext<dynamic> ReferenceDataHubClients { get; set; }
    }
}
