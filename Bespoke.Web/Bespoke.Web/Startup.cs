using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}