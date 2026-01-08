# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia a solução e restaura dependências
COPY *.sln .
COPY backend/*.csproj ./backend/
RUN dotnet restore

# Copia todo o backend e publica
COPY backend/. ./backend/
WORKDIR /app/backend
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "RondaSegurancaBack.dll"]
