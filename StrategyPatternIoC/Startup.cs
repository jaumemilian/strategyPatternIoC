using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StrategyPatternIoC.Interfaces;
using StrategyPatternIoC.Services;

namespace StrategyPatternIoC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();

            // Registrar las 2 implementaciones
            services.AddScoped<UserService1>();
            services.AddScoped<UserService2>();

            // Registrar la interfaz con un factory delegado
            services.AddScoped<IUserService>(serviceProvider =>
            {
                // Se valida en el momento que se necesita en el constructor y tenemos acceso a las claims
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var httpContext = httpContextAccessor.HttpContext;
                var claims = httpContext.User.Claims;

                // Aquí he hecho un random para ver en los logs como aleatoriamente carga uno u otro
                var rnd = new Random();
                if (rnd.Next(2) == 0)
                    return serviceProvider.GetService<UserService1>();
                else
                    return serviceProvider.GetService<UserService2>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
