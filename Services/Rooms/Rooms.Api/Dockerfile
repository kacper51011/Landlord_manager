#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Rooms/Rooms.Api/Rooms.Api.csproj", "Services/Rooms/Rooms.Api/"]
COPY ["Services/Rooms/Rooms.Application/Rooms.Application.csproj", "Services/Rooms/Rooms.Application/"]
COPY ["Services/Rooms/Rooms.Domain/Rooms.Domain.csproj", "Services/Rooms/Rooms.Domain/"]
COPY ["Services/Rooms/Rooms.Infrastructure/Rooms.Infrastructure.csproj", "Services/Rooms/Rooms.Infrastructure/"]
RUN dotnet restore "Services/Rooms/Rooms.Api/Rooms.Api.csproj"
COPY . .
WORKDIR "/src/Services/Rooms/Rooms.Api"
RUN dotnet build "Rooms.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rooms.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rooms.Api.dll"]