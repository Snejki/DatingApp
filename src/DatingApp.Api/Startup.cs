namespace DatingApp.Api
{
    using Autofac;
    using AutoMapper;
    using DatingApp.Api.Extensions;
    using DatingApp.Api.Filters;
    using DatingApp.Core;
    using DatingApp.Infrastructure.IoC;
    using DatingApp.Infrastructure.Services.Abstract;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opts =>
            {
                opts.Filters.Add(typeof(ModelStateValidator));
            }).AddFluentValidation(fv
                => fv.RegisterValidatorsFromAssemblyContaining<ModelStateValidator>());

            services.AddDbContext<DatingAppContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(IService));
            services.AddAutoMapper(typeof(IService));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new ContainerModule(this.Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseGlobalExceptionHandler();
            //app.UseSwaggerExt();
            //app.UseSwaggerUI();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
