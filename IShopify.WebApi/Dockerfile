FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["IShopify.WebApi/IShopify.WebApi.csproj", "IShopify.WebApi/"]
RUN dotnet restore "IShopify.WebApi/IShopify.WebApi.csproj"
COPY . .
WORKDIR "/src/IShopify.WebApi"
RUN dotnet build "IShopify.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IShopify.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IShopify.WebApi.dll"]