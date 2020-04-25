#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Backend/Clientes/BarberShop.Clientes.API/BarberShop.Clientes.API.csproj", "Backend/Clientes/BarberShop.Clientes.API/"]
COPY ["Backend/Clientes/BarberShop.Clientes.Aplicacao/BarberShop.Clientes.Aplicacao.csproj", "Backend/Clientes/BarberShop.Clientes.Aplicacao/"]
COPY ["Backend/Clientes/BarberShop.Clientes.Infra/BarberShop.Clientes.Infra.csproj", "Backend/Clientes/BarberShop.Clientes.Infra/"]
COPY ["Backend/Clientes/BarberShop.Clientes.Dominio/BarberShop.Clientes.Dominio.csproj", "Backend/Clientes/BarberShop.Clientes.Dominio/"]
RUN dotnet restore "Backend/Clientes/BarberShop.Clientes.API/BarberShop.Clientes.API.csproj"
COPY . .
WORKDIR "/src/Backend/Clientes/BarberShop.Clientes.API"
RUN dotnet build "BarberShop.Clientes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BarberShop.Clientes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarberShop.Clientes.API.dll"]