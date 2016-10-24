namespace Dcs.Application.Client
{
    using System;
    using System.Reactive;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    using Dcs.Application.Shared;

    using Microsoft.AspNet.SignalR.Client;

    internal class PricingServiceClient
    {
        public IObservable<PriceDto> GetSpotStreamForConnection(string currencyPair, IHubProxy pricingHubProxy)
        {
            return Observable.Create<PriceDto>(
                observer =>
                {
                    // subscribe to price feed first, otherwise there is a race condition 
                    var priceSubscription = pricingHubProxy.On<PriceDto>(
                        "OnNewPrice",
                        p =>
                        {
                            if (p.Symbol == currencyPair) observer.OnNext(p);
                        });

                    // send a subscription request
                    Console.WriteLine("Sending price subscription for currency pair {0}", currencyPair);
                    var subscription =
                        SendSubscription(currencyPair, pricingHubProxy)
                            .Subscribe(_ => Console.WriteLine("Subscribed to {0}", currencyPair), observer.OnError);

                    var unsubscriptionDisposable = Disposable.Create(
                        () =>
                        {
                            // send unsubscription when the observable gets disposed
                            Console.WriteLine("Sending price unsubscription for currency pair {0}", currencyPair);
                            SendUnsubscription(currencyPair, pricingHubProxy)
                                .Subscribe(
                                    _ => Console.WriteLine("Unsubscribed from {0}", currencyPair),
                                    ex =>
                                        Console.WriteLine(
                                            "An error occurred while sending unsubscription request for {0}:{1}",
                                            currencyPair,
                                            ex.Message));
                        });

                    return new CompositeDisposable { priceSubscription, unsubscriptionDisposable, subscription };
                }).Publish().RefCount();
        }

        private static IObservable<Unit> SendSubscription(string currencyPair, IHubProxy pricingHubProxy)
        {
            return
                Observable.FromAsync(
                    () =>
                        pricingHubProxy.Invoke(
                            "SubscribePriceStream",
                            new PriceSubscriptionRequestDto { CurrencyPair = currencyPair }));
        }

        private static IObservable<Unit> SendUnsubscription(string currencyPair, IHubProxy pricingHubProxy)
        {
            return
                Observable.FromAsync(
                    () =>
                        pricingHubProxy.Invoke(
                            "UnsubscribePriceStream",
                            new PriceSubscriptionRequestDto { CurrencyPair = currencyPair }));
        }
    }
}