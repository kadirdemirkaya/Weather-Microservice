FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5018 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateways/Api/ApiGateway.Api/ApiGateway.Api.csproj", "src/ApiGateways/Api/ApiGateway.Api/"]
COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
COPY ["src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/BuildingBlock.HealthCheck.csproj", "src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/"]
COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
RUN dotnet restore "./src/ApiGateways/Api/ApiGateway.Api/./ApiGateway.Api.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Api/ApiGateway.Api"
RUN dotnet build "./ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Api.dll"]