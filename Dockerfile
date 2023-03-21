FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7028

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiLoginUsuario/ApiLoginUsuario.csproj", "ApiLoginUsuario/"]
COPY ["Aplicacao/Aplicacao.csproj", "Aplicacao/"]
COPY ["Repositorio/Repositorio.csproj", "Repositorio/"]
COPY ["Dominio/Dominio.csproj", "Dominio/"]
RUN dotnet restore "ApiLoginUsuario/ApiLoginUsuario.csproj"
COPY . .
WORKDIR "/src/ApiLoginUsuario"
RUN dotnet build "ApiLoginUsuario.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiLoginUsuario.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm -rf /app/build
ENTRYPOINT ["dotnet", "ApiLoginUsuario.dll"]
