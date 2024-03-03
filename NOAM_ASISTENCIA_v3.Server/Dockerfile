#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NOAM_ASISTENCIA_v3.Server/NOAM_ASISTENCIA_v3.Server.csproj", "NOAM_ASISTENCIA_v3.Server/"]
RUN dotnet restore "./NOAM_ASISTENCIA_v3.Server/NOAM_ASISTENCIA_v3.Server.csproj"
COPY . .
WORKDIR "/src/NOAM_ASISTENCIA_v3.Server"
RUN dotnet build "./NOAM_ASISTENCIA_v3.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NOAM_ASISTENCIA_v3.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NOAM_ASISTENCIA_v3.Server.dll"]