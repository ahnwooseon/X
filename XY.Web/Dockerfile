FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les projets nécessaires et restaurer les dépendances
COPY ["XY.Web.csproj", "XY.Web/"]
COPY ["XY.ServiceDefaults.csproj", "XY.ServiceDefaults/"]
RUN dotnet restore "XY.Web/XY.Web.csproj"

# Copier tout le code source et construire
COPY ["XY.Web/", "XY.Web/"]
COPY ["XY.ServiceDefaults/", "XY.ServiceDefaults/"]

# Construire l’application
WORKDIR /src
RUN dotnet build "XY.Web/XY.Web.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
WORKDIR /src
RUN dotnet publish "XY.Web/XY.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

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
ENTRYPOINT ["dotnet", "XY.Web.dll"]
