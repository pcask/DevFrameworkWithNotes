using DevFramework.Core.Utilities.Common;
using DevFramework.Northwind.Business.Abstract;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            // Client senden IProductService istediğinde WcfProxy nesnesinin CreateChannel method'ı vasıtasıyla bir proxy üret.
            Bind<IProductService>().ToConstant(WcfProxy<IProductService>.CreateChannel());
        }
    }
}
