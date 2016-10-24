using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcs.Application.Server.Transport
{
    using Autofac;

    using Microsoft.AspNet.SignalR;

    public class AutofacSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IContainer _container;

        public AutofacSignalRDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            object instance;
            if (_container.TryResolve(serviceType, out instance))
            {
                return instance;
            }
            return base.GetService(serviceType);
        }
    }
}
