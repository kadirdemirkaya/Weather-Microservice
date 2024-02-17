FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ClientAndServerService/Services.ClientAndServerService.Api/Services.ClientAndServerService.Api.csproj", "src/Services/ClientAndServerService/Services.ClientAndServerService.Api/"]
COPY ["src/Services/ClientAndServerService/Services.ClientAndServerService/Services.ClientAndServerService.csproj", "src/Services/ClientAndServerService/Services.ClientAndServerService/"]
COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
COPY ["src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/BuildingBlock.HealthCheck.csproj", "src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/"]
COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
COPY ["src/BuildingBlocks/Mapper/BuildingBlock.Mapper/BuildingBlock.Mapper.csproj", "src/BuildingBlocks/Mapper/BuildingBlock.Mapper/"]
COPY ["src/BuildingBlocks/Validator/BuildingBlock.Validator/BuildingBlock.Validator.csproj", "src/BuildingBlocks/Validator/BuildingBlock.Validator/"]
RUN dotnet restore "./src/Services/ClientAndServerService/Services.ClientAndServerService.Api/./Services.ClientAndServerService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ClientAndServerService/Services.ClientAndServerService.Api"
RUN dotnet build "./Services.ClientAndServerService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services.ClientAndServerService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.ClientAndServerService.Api.dll"]