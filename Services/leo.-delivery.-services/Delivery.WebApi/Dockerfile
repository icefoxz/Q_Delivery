#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Delivery.WebApi/Delivery.WebApi.csproj", "Delivery.WebApi/"]
COPY ["Delivery.CommonInitializers/Delivery.CommonInitializers.csproj", "Delivery.CommonInitializers/"]
COPY ["Delivery.Services/Delivery.Services.csproj", "Delivery.Services/"]
COPY ["Delivery.IServices/Delivery.IServices.csproj", "Delivery.IServices/"]
COPY ["Delivery.EntityFramework.Core/Delivery.EntityFramework.Core.csproj", "Delivery.EntityFramework.Core/"]
COPY ["Delivery.Domains/Delivery.Domains.csproj", "Delivery.Domains/"]
COPY ["Delivery.Commons/Delivery.Commons.csproj", "Delivery.Commons/"]
RUN dotnet restore "Delivery.WebApi/Delivery.WebApi.csproj"
COPY . .
WORKDIR "/src/Delivery.WebApi"
RUN dotnet build "Delivery.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delivery.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery.WebApi.dll"]