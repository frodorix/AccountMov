#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountMovAPI/AccountMovAPI.csproj", "AccountMovAPI/"]
COPY ["CORE.Account/CORE.Account.csproj", "CORE.Account/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
RUN dotnet restore "AccountMovAPI/AccountMovAPI.csproj"
COPY . .
WORKDIR "/src/AccountMovAPI"
RUN rm -rf appsettings.Development.json
RUN dotnet build "AccountMovAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountMovAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountMovAPI.dll"]