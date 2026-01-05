FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY MintLynk.Web/*.csproj MintLynk.Web/
COPY MintLynk.Domain/*.csproj MintLynk.Domain/
COPY MintLynk.Application/*.csproj MintLynk.Application/
COPY MintLynk.Infrastructure/*.csproj MintLynk.Infrastructure/
COPY MintLynk.Persistence/*.csproj MintLynk.Persistence/

RUN dotnet restore

COPY . .
WORKDIR /src/MintLynk.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MintLynk.Web.dll"]
