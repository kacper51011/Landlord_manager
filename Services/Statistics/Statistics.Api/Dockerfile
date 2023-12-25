#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8003
EXPOSE 8007

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Statistics/Statistics.Api/Statistics.Api.csproj", "Services/Statistics/Statistics.Api/"]
COPY ["Services/Statistics/Statistics.Application/Statistics.Application.csproj", "Services/Statistics/Statistics.Application/"]
COPY ["Services/Contracts/Contracts.csproj", "Services/Contracts/"]
COPY ["Services/Statistics/Statistics.Domain/Statistics.Domain.csproj", "Services/Statistics/Statistics.Domain/"]
COPY ["Services/Statistics/Statistics.Infrastructure/Statistics.Infrastructure.csproj", "Services/Statistics/Statistics.Infrastructure/"]
RUN dotnet restore "Services/Statistics/Statistics.Api/Statistics.Api.csproj"
COPY . .
WORKDIR "/src/Services/Statistics/Statistics.Api"
RUN dotnet build "Statistics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Statistics.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Statistics.Api.dll"]