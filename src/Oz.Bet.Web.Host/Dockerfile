FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Oz.Bet/Oz.Bet.csproj","Oz.Bet/"]
COPY ["Oz.Bet.Web.Host/Oz.Bet.Web.Host.csproj", "Oz.Bet.Web.Host/"]
RUN dotnet restore "Oz.Bet.Web.Host/Oz.Bet.Web.Host.csproj"
COPY . .
WORKDIR "/src/Oz.Bet.Web.Host"
RUN dotnet build "Oz.Bet.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oz.Bet.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oz.Bet.Web.Host.dll"]
