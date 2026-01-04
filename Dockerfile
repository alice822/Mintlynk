# ========================
# Build stage
# ========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY Mintlynk.Web/*.csproj Mintlynk.Web/
COPY Mintlynk.Domain/*.csproj Mintlynk.Domain/
COPY Mintlynk.Application/*.csproj Mintlynk.Application/
COPY Mintlynk.Infrastructure/*.csproj Mintlynk.Infrastructure/
COPY Mintlynk.Persistence/*.csproj Mintlynk.Persistence/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/Mintlynk.Web
RUN dotnet publish -c Release -o /app/publish

# ========================
# Runtime stage
# ========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Mintlynk.Web.dll"]
