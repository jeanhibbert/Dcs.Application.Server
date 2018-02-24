using Dcs.Application.Server;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Dcs.Application.Server
{

    using Autofac;

    using Dcs.Application.Server.Pricing;

    public partial class App
    {

        public static IContainer Container;

        public void Start()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Build();

            // expose via static variable so SignalR can pick it up in Startup class 
            Container = container;

            var priceFeed = container.Resolve<IPriceFeed>();
            priceFeed.Start();

        }
    }
}