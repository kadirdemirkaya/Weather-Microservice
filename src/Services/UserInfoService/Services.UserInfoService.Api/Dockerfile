FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/UserInfoService/Services.UserInfoService.Api/Services.UserInfoService.Api.csproj", "src/Services/UserInfoService/Services.UserInfoService.Api/"]
COPY ["src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/BuildingBlock.HealthCheck.csproj", "src/BuildingBlocks/HealthCheck/BuildingBlock.HealthCheck/"]
COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
COPY ["src/Services/UserInfoService/Services.UserInfoService/Services.UserInfoService.csproj", "src/Services/UserInfoService/Services.UserInfoService/"]
COPY ["src/BuildingBlocks/Factory/BuildingBlock.Factory/BuildingBlock.Factory.csproj", "src/BuildingBlocks/Factory/BuildingBlock.Factory/"]
COPY ["src/BuildingBlocks/Dapper/BuildingBlock.Dapper/BuildingBlock.Dapper.csproj", "src/BuildingBlocks/Dapper/BuildingBlock.Dapper/"]
COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
COPY ["src/BuildingBlocks/Mapper/BuildingBlock.Mapper/BuildingBlock.Mapper.csproj", "src/BuildingBlocks/Mapper/BuildingBlock.Mapper/"]
COPY ["src/BuildingBlocks/MsSql/BuildingBlock.MsSql/BuildingBlock.MsSql.csproj", "src/BuildingBlocks/MsSql/BuildingBlock.MsSql/"]
COPY ["src/BuildingBlocks/Email/BuildingBlock.Email/BuildingBlock.Email.csproj", "src/BuildingBlocks/Email/BuildingBlock.Email/"]
COPY ["src/BuildingBlocks/Mongo/BuildingBlock.Mongo/BuildingBlock.Mongo.csproj", "src/BuildingBlocks/Mongo/BuildingBlock.Mongo/"]
COPY ["src/BuildingBlocks/PostgreSql/BuildingBlock.PostgreSql/BuildingBlock.PostgreSql.csproj", "src/BuildingBlocks/PostgreSql/BuildingBlock.PostgreSql/"]
COPY ["src/BuildingBlocks/RabbitMq/BuildingBlock.RabbitMq/BuildingBlock.RabbitMq.csproj", "src/BuildingBlocks/RabbitMq/BuildingBlock.RabbitMq/"]
COPY ["src/BuildingBlocks/Redis/BuildingBlock.Redis/BuildingBlock.Redis.csproj", "src/BuildingBlocks/Redis/BuildingBlock.Redis/"]
COPY ["src/BuildingBlocks/Jwt/BuildingBlock.Jwt/BuildingBlock.Jwt.csproj", "src/BuildingBlocks/Jwt/BuildingBlock.Jwt/"]
COPY ["src/BuildingBlocks/Validator/BuildingBlock.Validator/BuildingBlock.Validator.csproj", "src/BuildingBlocks/Validator/BuildingBlock.Validator/"]
RUN dotnet restore "./src/Services/UserInfoService/Services.UserInfoService.Api/./Services.UserInfoService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/UserInfoService/Services.UserInfoService.Api"
RUN dotnet build "./Services.UserInfoService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services.UserInfoService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.UserInfoService.Api.dll"]