#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8004

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Apartments/Apartments.API/Apartments.API.csproj", "Services/Apartments/Apartments.API/"]
COPY ["Services/Apartments/Apartments.Application/Apartments.Application.csproj", "Services/Apartments/Apartments.Application/"]
COPY ["Services/Apartments/Apartments.Domain/Apartments.Domain.csproj", "Services/Apartments/Apartments.Domain/"]
COPY ["Services/Apartments/Apartments.Infrastructure/Apartments.Infrastructure.csproj", "Services/Apartments/Apartments.Infrastructure/"]
RUN dotnet restore "Services/Apartments/Apartments.API/Apartments.API.csproj"
COPY . .
WORKDIR "/src/Services/Apartments/Apartments.API"
RUN dotnet build "Apartments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apartments.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apartments.API.dll"]