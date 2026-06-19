# Use ASP.NET 10 runtime as base
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80

# Use SDK 10 image for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Register.API/Register.API.csproj", "Register.API/"]
COPY ["Register.APLLICATION/Register.APPLICATION.csproj", "Register.APLLICATION/"]
COPY ["Register.DOMAIN/Register.DOMAIN.csproj", "Register.DOMAIN/"]
COPY ["Register.PERSISTANCE/Register.PERSISTANCE.csproj", "Register.PERSISTANCE/"]
RUN dotnet restore "Register.API/Register.API.csproj"

# Copy all sources and build the API project
COPY . .
WORKDIR "/src/Register.API"
RUN dotnet build "Register.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Register.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Generate final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]
