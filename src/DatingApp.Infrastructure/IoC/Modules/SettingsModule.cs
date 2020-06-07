namespace DatingApp.Infrastructure.IoC.Modules
{
    using Autofac;
    using DatingApp.Core.Settings;
    using Microsoft.Extensions.Configuration;

    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var jwtSettings = this.configuration.GetSection(typeof(JwtSettings).Name).Get<JwtSettings>();
            builder.Register(p => jwtSettings).SingleInstance();

            base.Load(builder);
        }
    }
}
