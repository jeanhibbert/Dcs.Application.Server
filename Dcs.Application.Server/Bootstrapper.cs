namespace Dcs.Application.Server
{
    using Autofac;

    using Dcs.Application.Server.Pricing;

    internal class Bootstrapper
    {
        public Bootstrapper()
        {
        }

        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ContextHolder>().As<IContextHolder>().SingleInstance();
            // pricing
            builder.RegisterType<PricePublisher>().As<IPricePublisher>().SingleInstance();
            builder.RegisterType<PriceFeed>().As<IPriceFeed>().SingleInstance();
            builder.RegisterType<PricingHub>().SingleInstance();

            return builder.Build();
        }
    }
}