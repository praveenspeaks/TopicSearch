FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TopicSearch/TopicSearch.csproj", "TopicSearch/"]
COPY ["TopicSearch/aspnetapp.pfx", "TopicSearch/"]
RUN dotnet restore "TopicSearch/TopicSearch.csproj"
COPY . .
WORKDIR "/src/TopicSearch"
RUN dotnet build "TopicSearch.csproj" -c Release -o /app/build

#Publish & Serve App
FROM build AS publish
RUN dotnet publish "TopicSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TopicSearch.dll"]