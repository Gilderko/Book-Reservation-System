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
    public class AutofacEFInfraConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Lifetime scope

            builder.RegisterType<BookRentalDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            // Perdependencyscope

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(Query<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();
        }
    }
}
