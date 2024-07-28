FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["hotel-clone-api.csproj", "."]
RUN dotnet restore "hotel-clone-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "hotel-clone-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hotel-clone-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Crear el directorio Images
RUN mkdir -p /app/Images
ENTRYPOINT ["dotnet", "hotel-clone-api.dll"]
