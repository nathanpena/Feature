using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Features.Common
{
    class AutoFacDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            object resolved;
            _container.TryResolve(serviceType, out resolved);
            return resolved;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            object resolved;
            Type type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _container.TryResolve(type, out resolved);
            return (IEnumerable<object>)resolved;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public AutoFacDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
        }

        private readonly IContainer _container;
    }
}
