# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../src/ApiMutants.PresentationLayer/ApiMutants.PresentationLayer.csproj", "../src/ApiMutants.PresentationLayer/"]
COPY ["../src/ApiMutants.Application.Interfaces/ApiMutants.Application.Interfaces.csproj", "../src/ApiMutants.Application.Interfaces/"]
COPY ["../src/ApiMutants.Domain/ApiMutants.Domain.csproj", "../src/ApiMutants.Domain/"]
COPY ["../src/ApiMutants.Application/ApiMutants.Application.csproj", "../src/ApiMutants.Application/"]
COPY ["../src/ApiMutants.Services/ApiMutants.Services.csproj", "../src/ApiMutants.Services/"]
RUN dotnet restore "./src/ApiMutants.PresentationLayer/ApiMutants.PresentationLayer.csproj"
COPY . .
WORKDIR "/src/src/ApiMutants.PresentationLayer"
RUN dotnet build "./ApiMutants.PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiMutants.PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiMutants.PresentationLayer.dll"]