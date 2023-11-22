#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8002
EXPOSE 8006

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Tenants/Tenants.Api/Tenants.Api.csproj", "Services/Tenants/Tenants.Api/"]
COPY ["Services/Tenants/Tenants.Application/Tenants.Application.csproj", "Services/Tenants/Tenants.Application/"]
COPY ["Services/Contracts/Contracts.csproj", "Services/Contracts/"]
COPY ["Services/Tenants/Tenants.Domain/Tenants.Domain.csproj", "Services/Tenants/Tenants.Domain/"]
COPY ["Services/Tenants/Tenants.Infrastructure/Tenants.Infrastructure.csproj", "Services/Tenants/Tenants.Infrastructure/"]
RUN dotnet restore "Services/Tenants/Tenants.Api/Tenants.Api.csproj"
COPY . .
WORKDIR "/src/Services/Tenants/Tenants.Api"
RUN dotnet build "Tenants.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tenants.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tenants.Api.dll"]