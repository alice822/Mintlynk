# Change 8.0 to 9.0 here
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . ./ 
RUN dotnet restore MintLynk.Web/MintLynk.Web.csproj
RUN dotnet publish MintLynk.Web/MintLynk.Web.csproj -c Release -o /app/publish

# Change 8.0 to 9.0 here as well
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "MintLynk.Web.dll"]