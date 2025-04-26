FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les projets nécessaires et restaurer les dépendances
COPY ["XY.ApiService/XY.ApiService.csproj", "XY.ApiService/"]
COPY ["XY.ServiceDefaults/XY.ServiceDefaults.csproj", "XY.ServiceDefaults/"]
RUN dotnet restore "XY.ApiService/XY.ApiService.csproj"

# Copier tout le code source et construire
COPY ["XY.ApiService", "XY.ApiService/"]
COPY ["XY.ServiceDefaults/", "XY.ServiceDefaults/"]

# Construire l’application
WORKDIR /src
RUN dotnet build "XY.ApiService/XY.ApiService.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
WORKDIR /src
RUN dotnet publish "XY.ApiService/XY.ApiService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Image finale optimisée
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Configuration d'environnement (production par défaut)
ENV ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true \
    ASPNETCORE_URLS=http://+:8080

# Copier les fichiers publiés
COPY --from=publish /app/publish .

# Point d'entrée de l'application
ENTRYPOINT ["dotnet", "XY.ApiService.dll"]
