#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Oz.Bet/Oz.Bet.csproj", "Oz.Bet/"]
COPY ["Oz.Bet.DataProvider/Oz.Bet.DataProvider.csproj", "Oz.Bet.DataProvider/"]
COPY ["Oz.Bet.DataProvider.Host/Protos/bulletin.proto", "Oz.Bet.DataProvider.Host/Protos/"]
COPY ["Oz.Bet.DataProvider.Host/Oz.Bet.DataProvider.Host.csproj", "Oz.Bet.DataProvider.Host/"]
RUN dotnet restore "Oz.Bet.DataProvider.Host/Oz.Bet.DataProvider.Host.csproj"
COPY . .
WORKDIR "/src/Oz.Bet.DataProvider.Host"
RUN dotnet build "Oz.Bet.DataProvider.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oz.Bet.DataProvider.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oz.Bet.DataProvider.Host.dll"]