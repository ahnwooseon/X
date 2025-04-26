# Étape 1 : Construction de l'application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Installer le workload aspire
RUN dotnet workload install aspire

# Copie du projet Aspire (.csproj)
COPY ["XY.AppHost/XY.AppHost.csproj", "XY.AppHost/"]
RUN dotnet restore "XY.AppHost/XY.AppHost.csproj"

# Copie du code source
COPY . .

# Construction du projet en mode Release
WORKDIR "/src/XY.AppHost"
RUN dotnet build "XY.AppHost.csproj" -c Release -o /app/build

# Étape 2 : Publication de l'application
FROM build AS publish
RUN dotnet publish "XY.AppHost.csproj" -c Release -o /app/publish

# Étape 3 : Image finale pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Copie des fichiers publiés dans le répertoire final
COPY --from=publish /app/publish .

# Lancer l'application Aspire
ENTRYPOINT ["dotnet", "XY.AppHost.dll"]
