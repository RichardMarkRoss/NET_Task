FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["UserGroup.Api/UserGroup.Api.csproj", "UserGroup.Api/"]
COPY ["UserGroup.Infrastructure/UserGroup.Infrastructure.csproj", "UserGroup.Infrastructure/"]
COPY ["UserGroup.Domain/UserGroup.Domain.csproj", "UserGroup.Domain/"]
COPY ["UserGroup.Migrator/UserGroup.Migrator.csproj", "UserGroup.Migrator/"]
RUN dotnet restore "UserGroup.Api/UserGroup.Api.csproj"

COPY . .
WORKDIR "/src/UserGroup.Api"
RUN dotnet build "UserGroup.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserGroup.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserGroup.Api.dll"]
