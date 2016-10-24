using Dcs.Application.Server;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Dcs.Application.Server
{
    using System;

    using Microsoft.Owin.Hosting;

    internal class Program
    {
        private static void Main()
        {
            var app = new App();
            app.Start();

            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            const string url = @"http://localhost:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}