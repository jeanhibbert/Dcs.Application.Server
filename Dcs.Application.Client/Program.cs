using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcs.Application.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Connection connection = new Connection())
            {
                var pricingClient = new PricingServiceClient();

                var subscription = pricingClient.GetSpotStreamForConnection("USDGBP", connection.PricingHubProxy)
                    .Subscribe(p => 
                        Console.WriteLine(p.ToString())
                    );

                Console.ReadKey();
                subscription.Dispose();
            }
        }
    }
}
