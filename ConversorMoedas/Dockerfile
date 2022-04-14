#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConversorMoedas/ConversorMoedas.Api.csproj", "ConversorMoedas/"]
COPY ["ConversorMoedas.Repository/ConversorMoedas.Repository.csproj", "ConversorMoedas.Repository/"]
COPY ["ConversorMoedas.Domain/ConversorMoedas.Domain.csproj", "ConversorMoedas.Domain/"]
COPY ["ConversorMoedas.Common/ConversorMoedas.Common.csproj", "ConversorMoedas.Common/"]
COPY ["ConversorMoedas.Services/ConversorMoedas.Services.csproj", "ConversorMoedas.Services/"]
RUN dotnet restore "ConversorMoedas/ConversorMoedas.Api.csproj"
COPY . .
WORKDIR "/src/ConversorMoedas"
RUN dotnet build "ConversorMoedas.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConversorMoedas.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConversorMoedas.Api.dll"]