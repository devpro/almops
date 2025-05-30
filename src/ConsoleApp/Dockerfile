#FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
FROM registry.suse.com/bci/dotnet-aspnet:9.0 AS base
USER app
WORKDIR /app

#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
FROM registry.suse.com/bci/dotnet-sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/", "./"]
RUN dotnet restore "ConsoleApp/ConsoleApp.csproj"
COPY . .
WORKDIR "/src/ConsoleApp"
RUN dotnet build "ConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "almops.dll"]
