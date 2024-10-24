FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /SnakeGame
COPY SnakeGame/SnakeGame.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish SnakeGame/SnakeGame.csproj -c release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SnakeGame.dll"]

HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 \
    CMD curl -f http://localhost || exit 1

EXPOSE 8080