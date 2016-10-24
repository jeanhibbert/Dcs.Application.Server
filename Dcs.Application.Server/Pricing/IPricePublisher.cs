namespace Dcs.Application.Server.Pricing
{
    using System.Threading.Tasks;

    using Dcs.Application.Shared;

    public interface IPricePublisher
    {
        Task Publish(PriceDto price);
        long TotalPricesPublished { get; }
    }
}