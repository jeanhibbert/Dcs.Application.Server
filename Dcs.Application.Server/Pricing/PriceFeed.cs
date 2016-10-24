using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcs.Application.Server.Pricing
{
    using System.Reactive.Linq;

    using Dcs.Application.Shared;

    public class PriceFeed : IPriceFeed
    {
        private readonly IPricePublisher _pricePublisher;
        public PriceFeed(IPricePublisher pricePublisher)
        {
            this._pricePublisher = pricePublisher;
        }
        
        public void Start()
        {
            var subscription = Observable.Interval(TimeSpan.FromSeconds(10)).Subscribe(
                x =>
                {
                    var price = new PriceDto
                    {
                        QuoteId = x,
                        Ask = new Random().Next(0, 10),
                        Symbol = "USDGBP"
                    };
                    this._pricePublisher.Publish(price);
                });
            
        }
    }
}
