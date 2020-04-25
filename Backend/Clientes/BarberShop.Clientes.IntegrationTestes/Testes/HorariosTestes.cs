using BarberShop.Clientes.API;
using BarberShop.Clientes.Dominio.Entidades;
using BarberShop.Clientes.Dominio.Models.Request;
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
using System.Net.Http;
using System.Text;
using Xunit;

namespace BarberShop.Clientes.IntegrationTestes.Testes
{
    public class HorariosTestes
    {
        [Fact]
        public async void Deve_Marcar_Um_Horario()
        {
            DateTime dataHorario = new DateTime(2019, 1, 1);
            int clientId = 1;
            int servicoId = 1;
            var horarioEsperado = new {
                ClienteId = clientId,
                ServicoId = servicoId,
                HorarioMarcado = dataHorario,
                Id = 1,
                IsNew = false
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
            db.Set<Cliente>().AddRange(ClienteMock.ObterClientesMock());
            db.Set<Servico>().AddRange(ServicosMock.ObterServicosMock());
            db.SaveChanges();
            var client = server.CreateClient();

            MarcarHorarioRequest request = new MarcarHorarioRequest()
            {
                ClientId = clientId,
                ServicoId = servicoId,
                HorarioMarcado = dataHorario
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/horarios/marcar", content);
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<Horario> horarioResponse = JsonConvert.DeserializeObject<RequestResponse<Horario>>(stringRequest);
            Assert.Empty(horarioResponse.Erros);
            horarioEsperado.ShouldMatch(horarioResponse.Dado);
        }

        [Fact]
        public async void Deve_Obter_Agenda_De_Horarios()
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
            db.Set<Cliente>().AddRange(ClienteMock.ObterClientesMock());
            db.Set<Horario>().AddRange(HorarioMock.ObterAgendaMock());
            db.SaveChanges();
            var client = server.CreateClient();
            var response = await client.GetAsync("/api/horarios/agenda?dataInicio=01/01/2019&dataFim=01/01/2019");
            response.EnsureSuccessStatusCode();
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<IEnumerable<Horario>> clientesRespponse = JsonConvert.DeserializeObject<RequestResponse<IEnumerable<Horario>>>(stringRequest);
            Assert.Empty(clientesRespponse.Erros);
            Assert.NotEmpty(clientesRespponse.Dado);
        }

        [Fact]
        public async void Deve_Tratar_Exception()
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
            var client = server.CreateClient();
            var response = await client.GetAsync("/api/horarios/agenda");
            var stringRequest = await response.Content.ReadAsStringAsync();
            RequestResponse<IEnumerable<Horario>> clientesRespponse = JsonConvert.DeserializeObject<RequestResponse<IEnumerable<Horario>>>(stringRequest);
            Assert.NotEmpty(clientesRespponse.Erros);
        }
    }
}
