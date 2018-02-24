using System;
using System.Threading.Tasks;

namespace Dcs.Application.Server
{
    using Dcs.Application.Shared;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName(ServiceConstants.Server.PricingHub)]
    public class PricingHub : Hub
    {
        private readonly IContextHolder _contextHolder;
        public const string PriceStreamGroupPattern = "Pricing/{0}";

        public PricingHub(IContextHolder contextHolder)
        {
            _contextHolder = contextHolder;
        }

        [HubMethodName(ServiceConstants.Server.SubscribePriceStream)]
        public async Task SubscribePriceStream(PriceSubscriptionRequestDto request)
        {
            _contextHolder.PricingHubClient = Clients;

            Console.WriteLine("Received subscription request {0} from connection {1}", request, Context.ConnectionId);

            // add client to this group
            var groupName = string.Format(PriceStreamGroupPattern, request.CurrencyPair);
            await Groups.Add(Context.ConnectionId, groupName);
            Console.WriteLine("Connection {0} added to group '{1}'", Context.ConnectionId, groupName);

            // send current price to client
            var firstPrice = new PriceDto { Symbol = "TEST"};
            await Clients.Caller.OnNewPrice(firstPrice);
            Console.WriteLine("Snapshot published to {0}: {1}", Context.ConnectionId, firstPrice);
        }

        [HubMethodName(ServiceConstants.Server.UnsubscribePriceStream)]
        public async Task UnsubscribePriceStream(PriceSubscriptionRequestDto request)
        {
            Console.WriteLine("Received unsubscription request {0} from connection {1}", request, Context.ConnectionId);

            // remove client from the group
            var groupName = string.Format(PriceStreamGroupPattern, request.CurrencyPair);
            await Groups.Remove(Context.ConnectionId, groupName);
            Console.WriteLine("Connection {0} removed from group '{1}'", Context.ConnectionId, groupName);
        }
    }
}
