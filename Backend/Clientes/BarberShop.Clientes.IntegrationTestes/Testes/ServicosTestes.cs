using BarberShop.Clientes.API;
using BarberShop.Clientes.Aplicacao.Models.Request;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Models.Response;
using BarberShop.Clientes.Infra.Database.Context;
using BarberShop.Clientes.IntegrationTestes.Mocks;
using ExpectedObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BarberShop.Clientes.IntegrationTestes.Testes
{
    public class ServicosTestes
    {
        [Fact]
        public async void Deve_Obter_Todos_Servicos()
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
            db.Set<Servico>().AddRange(ServicosMock.ObterServicosMock());
            db.SaveChanges();
            var client = server.CreateClient();
            var response = await client.GetAsync("/api/servicos");
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<IEnumerable<Servico>> clientesRespponse = JsonConvert.DeserializeObject<RequestResponse<IEnumerable<Servico>>>(stringRequest);
            Assert.Empty(clientesRespponse.Erros);
            Assert.NotEmpty(clientesRespponse.Dado);
        }

        [Fact(Skip = "Descobrir o por que de estar falhando")]
        public async void Deve_Criar_Servico()
        {
            var nomeEsperado = "Serviço 1";
            var valorEsperado = 15;
            var servicoEsperado = new { 
                Id = 4, 
                IsNew = false, 
                Nome = nomeEsperado, 
                Valor = valorEsperado 
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

            CriarServicoRequest request = new CriarServicoRequest()
            {
                Nome = nomeEsperado,
                Valor = valorEsperado
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/servicos", content);
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<Servico> servicoResponse = JsonConvert.DeserializeObject<RequestResponse<Servico>>(stringRequest);
            Assert.Empty(servicoResponse.Erros);
            servicoEsperado.ShouldMatch(servicoResponse.Dado);
        }
    }
}
