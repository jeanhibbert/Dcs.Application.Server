namespace Dcs.Application.Server.Pricing
{
    using System;
    using System.Threading.Tasks;

    using Dcs.Application.Shared;

    public class PricePublisher : IPricePublisher
    {
        private readonly IContextHolder _contextHolder;

        public PricePublisher(IContextHolder contextHolder)
        {
            this._contextHolder = contextHolder;
        }

        public async Task Publish(PriceDto price)
        {
            var context = this._contextHolder.PricingHubClient;
            if (context == null) return;

            this.TotalPricesPublished++;
            var groupName = string.Format(PricingHub.PriceStreamGroupPattern, price.Symbol);
            try
            {
                await context.Group(groupName).OnNewPrice(price);
                Console.WriteLine("Published price to group {0}: {1}", groupName, price);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while publishing price to group {groupName}: {price}", e);
            }
        }

        public long TotalPricesPublished { get; private set; }
    }
}