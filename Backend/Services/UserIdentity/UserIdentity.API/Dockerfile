#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/UserIdentity/UserIdentity.API/UserIdentity.API.csproj", "Services/UserIdentity/UserIdentity.API/"]
COPY ["Services/UserIdentity/UserIdentity.API/NuGet.config", "Services/UserIdentity/UserIdentity.API/"]
RUN dotnet restore --configfile "Services/UserIdentity/UserIdentity.API/NuGet.config" "Services/UserIdentity/UserIdentity.API/UserIdentity.API.csproj"
COPY . .
WORKDIR "/src/Services/UserIdentity/UserIdentity.API"
RUN dotnet build "NuGet.config" "UserIdentity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserIdentity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserIdentity.API.dll"]