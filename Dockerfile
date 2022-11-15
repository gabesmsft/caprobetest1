FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

# copy csproj files
COPY BadPerf/*.csproj ./BadPerf/
RUN dotnet restore "BadPerf/BadPerf.csproj"

# copy everything else and restore and build app
COPY . .
WORKDIR /source/BadPerf
RUN dotnet build "BadPerf.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BadPerf.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

# ENTRYPOINT export ASPNETCORE_ENVIRONMENT=Development && exec dotnet "BadPerf.dll"
# ENTRYPOINT ["dotnet", "BadPerf.dll"]

COPY entrypoint.sh /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
CMD ["dotnet", "BadPerf.dll"]