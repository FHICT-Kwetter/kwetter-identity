FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# workaround for https://github.com/grpc/grpc/issues/24153
RUN apt-get update && apt-get install -y libc-dev && apt-get clean

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["IdentityServer.Api/IdentityServer.Api.csproj", "IdentityServer.Api/"]
COPY ["IdentityServer.Service/IdentityServer.Service.csproj", "IdentityServer.Service/"]
COPY ["IdentityServer.Domain/IdentityServer.Domain.csproj", "IdentityServer.Domain/"]
COPY ["IdentityServer.PubSub/IdentityServer.PubSub.csproj", "IdentityServer.PubSub/"]
COPY ["IdentityServer.Data/IdentityServer.Data.csproj", "IdentityServer.Data/"]

RUN dotnet restore "IdentityServer.Api/IdentityServer.Api.csproj"
COPY . .
WORKDIR /src/IdentityServer.Api

RUN dotnet build "IdentityServer.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "IdentityServer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.Api.dll"]