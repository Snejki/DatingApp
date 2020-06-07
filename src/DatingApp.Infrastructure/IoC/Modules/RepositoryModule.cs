namespace DatingApp.Infrastructure.IoC.Modules
{
    using System.Reflection;
    using Autofac;
    using DatingApp.Core.Repositories.Abstract;

    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterGeneric(typeof(ISaveRepository<>))
                .As(typeof(ISaveRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
