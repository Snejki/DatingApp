namespace DatingApp.Infrastructure.IoC
{
    using Autofac;
    using DatingApp.Infrastructure.IoC.Modules;
    using Microsoft.Extensions.Configuration;

    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public ContainerModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new SettingsModule(this.configuration));
        }
    }
}
