using BarberShop.Clientes.API.Filters;
using BarberShop.Clientes.Dominio.Repositorios.Interfaces;
using BarberShop.Clientes.Dominio.Services;
using BarberShop.Clientes.Dominio.Services.Interfaces;
using BarberShop.Clientes.Infra.Database.Context;
using BarberShop.Clientes.Infra.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BarberShop.Clientes.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config => 
            {
                config.Filters.Add(new ResponseFilter());
                config.Filters.Add(new ValidateModelStateAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BarberShop API", Version = "v1" });
            });

            services.AddScoped<IHorarioRepositorio, HorarioRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IServicoRepositorio, ServicoRepositorio>();
            services.AddScoped<IHorariosService, HorariosService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<BarberShopContext>(x =>
               x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
               y => y.MigrationsHistoryTable("HistoryMigrations"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberShop V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<BarberShopContext>())
            {
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
