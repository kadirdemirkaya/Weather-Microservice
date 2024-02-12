##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY .env.production . 
#COPY ["src/Services/LocationService/Services.LocationService.Api/Services.LocationService.Api.csproj", "src/Services/LocationService/Services.LocationService.Api/"]
#COPY ["src/Services/LocationService/Services.LocationService/Services.LocationService.csproj", "src/Services/LocationService/Services.LocationService/"]
#COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
#COPY ["src/BuildingBlocks/Factory/BuildingBlock.Factory/BuildingBlock.Factory.csproj", "src/BuildingBlocks/Factory/BuildingBlock.Factory/"]
#COPY ["src/BuildingBlocks/Dapper/BuildingBlock.Dapper/BuildingBlock.Dapper.csproj", "src/BuildingBlocks/Dapper/BuildingBlock.Dapper/"]
#COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
#COPY ["src/BuildingBlocks/Mapper/BuildingBlock.Mapper/BuildingBlock.Mapper.csproj", "src/BuildingBlocks/Mapper/BuildingBlock.Mapper/"]
#COPY ["src/BuildingBlocks/MsSql/BuildingBlock.MsSql/BuildingBlock.MsSql.csproj", "src/BuildingBlocks/MsSql/BuildingBlock.MsSql/"]
#COPY ["src/BuildingBlocks/Mongo/BuildingBlock.Mongo/BuildingBlock.Mongo.csproj", "src/BuildingBlocks/Mongo/BuildingBlock.Mongo/"]
#RUN dotnet restore "./src/Services/LocationService/Services.LocationService.Api/./Services.LocationService.Api.csproj"
#COPY . .
#WORKDIR "/src/src/Services/LocationService/Services.LocationService.Api"
#RUN dotnet build "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#
#ENV ASPNETCORE_ENVIRONMENT=Production
#RUN dotnet publish "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#CMD ["sh", "-c", "dotnet Services.LocationService.Api.dll"]
##ENTRYPOINT ["dotnet", "Services.LocationService.Api.dll"]



#
#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY .env.production . 
#COPY ["src/Services/LocationService/Services.LocationService.Api/Services.LocationService.Api.csproj", "src/Services/LocationService/Services.LocationService.Api/"]
#COPY ["src/Services/LocationService/Services.LocationService/Services.LocationService.csproj", "src/Services/LocationService/Services.LocationService/"]
#COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
#COPY ["src/BuildingBlocks/Factory/BuildingBlock.Factory/BuildingBlock.Factory.csproj", "src/BuildingBlocks/Factory/BuildingBlock.Factory/"]
#COPY ["src/BuildingBlocks/Dapper/BuildingBlock.Dapper/BuildingBlock.Dapper.csproj", "src/BuildingBlocks/Dapper/BuildingBlock.Dapper/"]
#COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
#COPY ["src/BuildingBlocks/Mapper/BuildingBlock.Mapper/BuildingBlock.Mapper.csproj", "src/BuildingBlocks/Mapper/BuildingBlock.Mapper/"]
#COPY ["src/BuildingBlocks/MsSql/BuildingBlock.MsSql/BuildingBlock.MsSql.csproj", "src/BuildingBlocks/MsSql/BuildingBlock.MsSql/"]
#COPY ["src/BuildingBlocks/Mongo/BuildingBlock.Mongo/BuildingBlock.Mongo.csproj", "src/BuildingBlocks/Mongo/BuildingBlock.Mongo/"]
#RUN dotnet restore "./src/Services/LocationService/Services.LocationService.Api/./Services.LocationService.Api.csproj"
#COPY . .
#WORKDIR "/src/src/Services/LocationService/Services.LocationService.Api"
#RUN dotnet build "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#
#RUN dotnet publish "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Services.LocationService.Api.dll"]







FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY .env.production . 
COPY ["src/Services/LocationService/Services.LocationService.Api/Services.LocationService.Api.csproj", "src/Services/LocationService/Services.LocationService.Api/"]
COPY ["src/Services/LocationService/Services.LocationService/Services.LocationService.csproj", "src/Services/LocationService/Services.LocationService/"]
COPY ["src/BuildingBlocks/Base/BuildingBlock.Base/BuildingBlock.Base.csproj", "src/BuildingBlocks/Base/BuildingBlock.Base/"]
COPY ["src/BuildingBlocks/Factory/BuildingBlock.Factory/BuildingBlock.Factory.csproj", "src/BuildingBlocks/Factory/BuildingBlock.Factory/"]
COPY ["src/BuildingBlocks/Dapper/BuildingBlock.Dapper/BuildingBlock.Dapper.csproj", "src/BuildingBlocks/Dapper/BuildingBlock.Dapper/"]
COPY ["src/BuildingBlocks/Logger/BuildingBlock.Logger/BuildingBlock.Logger.csproj", "src/BuildingBlocks/Logger/BuildingBlock.Logger/"]
COPY ["src/BuildingBlocks/Mapper/BuildingBlock.Mapper/BuildingBlock.Mapper.csproj", "src/BuildingBlocks/Mapper/BuildingBlock.Mapper/"]
COPY ["src/BuildingBlocks/MsSql/BuildingBlock.MsSql/BuildingBlock.MsSql.csproj", "src/BuildingBlocks/MsSql/BuildingBlock.MsSql/"]
COPY ["src/BuildingBlocks/Mongo/BuildingBlock.Mongo/BuildingBlock.Mongo.csproj", "src/BuildingBlocks/Mongo/BuildingBlock.Mongo/"]
RUN dotnet restore "./src/Services/LocationService/Services.LocationService.Api/./Services.LocationService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/LocationService/Services.LocationService.Api"
RUN dotnet build "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./Services.LocationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

ENV ASPNETCORE_ENVIRONMENT=Production

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "Services.LocationService.Api.dll", "--no-launch-profile", "--urls", "http://0.0.0.0:80"]