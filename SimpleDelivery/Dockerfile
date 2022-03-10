FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app
COPY *.sln .
COPY src ./src

RUN dotnet restore
RUN dotnet publish -c release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]