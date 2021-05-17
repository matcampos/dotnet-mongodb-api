FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 4040

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MongodbApi/MongodbApi.csproj", "MongodbApi/"]
RUN dotnet restore "MongodbApi/MongodbApi.csproj"
COPY . .
WORKDIR "/src/MongodbApi"
RUN dotnet build "MongodbApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MongodbApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongodbApi.dll"]