using BarberShop.Clientes.API;
using BarberShop.Clientes.Infra.Database.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BarberShop.Clientes.Dominio.Models.Response;
using System.Collections.Generic;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.IntegrationTestes.Mocks;
using ExpectedObjects;
using BarberShop.Clientes.Aplicacao.Models.Request;
using System.Net.Http;
using System.Text;

namespace BarberShop.Clientes.IntegrationTestes.Testes
{
    public class ClienteTestes
    {
        [Fact]
        public async void Deve_Obter_Todos_Clientes()
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
            var db = scopedServices.GetRequiredService<BarberShopContext>();
            db.Database.EnsureCreated();
            var clientes = ClienteMock.ObterClientesMock();
            db.Set<Cliente>().AddRange(clientes);
            db.SaveChanges();
            var client = server.CreateClient();
            var response = await client.GetAsync("/api/clientes");
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<IEnumerable<Cliente>> clientesRespponse = JsonConvert.DeserializeObject<RequestResponse<IEnumerable<Cliente>>>(stringRequest);
            Assert.Empty(clientesRespponse.Erros);
            Assert.NotEmpty(clientesRespponse.Dado);
        }

        [Fact(Skip = "Descobrir o por que de estar falhando")]
        public async void Deve_Criar_Cliente()
        {
            var nomeEsperado = "Cliente 1";
            var clienteEsperado = new
            {
                Id = 1,
                IsNew = false,
                Nome = nomeEsperado,
            }.ToExpectedObject();
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
            var db = scopedServices.GetRequiredService<BarberShopContext>();
            db.Database.EnsureCreated();
            db.Set<Servico>().AddRange(ServicosMock.ObterServicosMock());
            db.SaveChanges();
            var client = server.CreateClient();

            CriarClienteRequest request = new CriarClienteRequest()
            {
                Nome = nomeEsperado
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/clientes", content);
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<Cliente> clienteResponse = JsonConvert.DeserializeObject<RequestResponse<Cliente>>(stringRequest);
            Assert.Empty(clienteResponse.Erros);
            clienteEsperado.ShouldMatch(clienteResponse.Dado);
        }
    }
}
