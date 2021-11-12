using Autofac;
using AutoMapper;
using BL.QueryObjects;
using BL.Services;
using EFInfrastructure;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace BL.Config
{
    public class AutofacBLConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacEFInfraConfig());

            // Per dependency

            builder.RegisterGeneric(typeof(CRUDService<,>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(QueryObject<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Services")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Facades")
                .AsSelf()
                .InstancePerDependency();

            // Single Instance

            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacEFInfraConfig());

            // Per dependency

            builder.RegisterGeneric(typeof(CRUDService<,>))
                 .InstancePerDependency();

            builder.RegisterGeneric(typeof(QueryObject<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Services")
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Facades")
                .AsSelf()
                .InstancePerDependency();

            // Single Instance

            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
