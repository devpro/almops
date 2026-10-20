# syntax=docker/dockerfile:1

FROM registry.suse.com/bci/dotnet-aspnet:10.0.13 AS base
USER $APP_UID
WORKDIR /app

FROM registry.suse.com/bci/dotnet-sdk:10.0.13 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Directory.*.props .
COPY ./src ./src
RUN dotnet restore "src/ConsoleApp/ConsoleApp.csproj"
RUN dotnet build "src/ConsoleApp/ConsoleApp.csproj" --no-restore -c "$BUILD_CONFIGURATION"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet publish "src/ConsoleApp/ConsoleApp.csproj" --no-build -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "almops.dll"]
