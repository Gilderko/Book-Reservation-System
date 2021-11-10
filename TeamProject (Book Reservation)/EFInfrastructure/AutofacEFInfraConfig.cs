using Autofac;
using DAL;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    public class AutofacEFInfraConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWorkProvider>().AsSelf().SingleInstance();
            

            return builder.Build();
        }
    }
}
