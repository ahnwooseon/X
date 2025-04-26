# Étape 1 : Construction de l'application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Installer le workload aspire
RUN dotnet workload install aspire

# Copier seulement les fichiers de projet et restaurer
COPY ["XY.AppHost/XY.AppHost.csproj", "XY.AppHost/"]
RUN dotnet restore "XY.AppHost/XY.AppHost.csproj"

# Copier tout le code source
COPY . .

# Construire l'application
WORKDIR "/src/XY.AppHost"
RUN dotnet build "XY.AppHost.csproj" -c Release -o /app/build

# Étape 2 : Publication de l'application
FROM build AS publish
WORKDIR "/src/XY.AppHost"
RUN dotnet publish "XY.AppHost.csproj" -c Release -o /app/publish

# (Optionnel : vérifier que ça a marché)
RUN ls -alh /app/publish

# Étape 3 : Image finale pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80

# Copier les fichiers publiés
COPY --from=publish /app/publish .

# Démarrer l'application
ENTRYPOINT ["dotnet", "XY.AppHost.dll"]
