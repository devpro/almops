# syntax=docker/dockerfile:1

FROM registry.suse.com/bci/dotnet-aspnet:10.0.14 AS base
# The latest base image lags SUSE-SU-2026:5068-1 (curl) and SUSE-SU-2026:4945-1 (brotli): remove this update once a rebuilt base image scans clean.
RUN zypper -n up curl libcurl4 libbrotlicommon1 libbrotlidec1 && zypper -n clean --all
USER $APP_UID
WORKDIR /app

FROM registry.suse.com/bci/dotnet-sdk:10.0.14 AS build
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
