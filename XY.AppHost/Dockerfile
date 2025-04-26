# Étape 1 : Construction de l'application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Installer le workload aspire
RUN dotnet workload install aspire

# Copier uniquement les fichiers nécessaires pour restaurer au début
COPY XY.AppHost/XY.AppHost.csproj XY.AppHost/
RUN dotnet restore XY.AppHost/XY.AppHost.csproj

# Copier tout le reste après la restauration
COPY . .

# Construction du projet en mode Release
WORKDIR /src/XY.AppHost
RUN dotnet build -c Release -o /app/build

# Étape 2 : Publication de l'application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Étape 3 : Image finale pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80

# Copie du résultat publié
COPY --from=publish /app/publish .

# Lancer l'application
ENTRYPOINT ["dotnet", "XY.AppHost.dll"]
