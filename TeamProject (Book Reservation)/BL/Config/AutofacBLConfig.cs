using Autofac;
using AutoMapper;
using BL.QueryObjects;
using BL.Services;
using BL.Services.Implementations;
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

            builder.RegisterGeneric(typeof(QueryObject<,>))
                .InstancePerDependency();

            // Services 

            builder.RegisterGeneric(typeof(CRUDService<,>))
                .As(typeof(ICRUDService<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Services.Implementations" && !t.Name.StartsWith("CRUD"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();

            // Facades 

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

            builder.RegisterGeneric(typeof(QueryObject<,>))
                 .InstancePerDependency();

            // Services 

            builder.RegisterGeneric(typeof(CRUDService<,>))
                .As(typeof(ICRUDService<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Services.Implementations" && !t.Name.StartsWith("CRUD"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();

            // Facades 

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
