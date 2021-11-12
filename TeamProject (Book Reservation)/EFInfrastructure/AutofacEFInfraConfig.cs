using Autofac;
using DAL;
using Infrastructure;
using Infrastructure.Query;

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
