FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY . /src
WORKDIR /src
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]