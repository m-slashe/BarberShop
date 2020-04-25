using BarberShop.Clientes.API;
using BarberShop.Clientes.Infra.Database.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BarberShop.Clientes.IntegrationTestes.Testes
{
    public class SwaggerTestes
    {
        [Fact]
        public async void Deve_Carregar_Swagger_Corretamente()
        {
            var server = new TestServer(new WebHostBuilder()
               .ConfigureServices(services =>
               {
                   services.AddDbContext<BarberShopContext>(options =>
                   {
                       options.UseInMemoryDatabase("Test");
                   });
               })
               .UseStartup<Startup>());
            var scope = server.Host.Services.CreateScope();

            var scopedServices = scope.ServiceProvider;            
            var client = server.CreateClient();
            var response = await client.GetAsync("/swagger/v1/swagger.json");
            response.EnsureSuccessStatusCode();
        }
    }
}
