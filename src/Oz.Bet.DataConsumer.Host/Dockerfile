#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Oz.Bet/Oz.Bet.csproj","Oz.Bet/"]
COPY ["Oz.Bet.DataConsumer.Host/Protos/bulletin.proto", "Oz.Bet.DataConsumer.Host/Protos/"]
COPY ["Oz.Bet.DataConsumer.Host/Oz.Bet.DataConsumer.Host.csproj", "Oz.Bet.DataConsumer.Host/"]
RUN dotnet restore "Oz.Bet.DataConsumer.Host/Oz.Bet.DataConsumer.Host.csproj"
COPY . .
WORKDIR "/src/Oz.Bet.DataConsumer.Host"
RUN dotnet build "Oz.Bet.DataConsumer.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oz.Bet.DataConsumer.Host.csproj" -c Release -o /app/publish

FROM base AS final
#RUN apt-get update -y
#RUN apt-get install iputils-ping -y
#RUN apt-get install telnet -y
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oz.Bet.DataConsumer.Host.dll"]
