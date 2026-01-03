FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Change this line:
COPY . ./ 

RUN dotnet restore MintLynk.Web/MintLynk.Web.csproj
RUN dotnet publish MintLynk.Web/MintLynk.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "MintLynk.Web.dll"]