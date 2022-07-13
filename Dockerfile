#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY . .
RUN dotnet restore "./TopicSearch/TopicSearch.csproj" --disable-parallel
RUN dotnet publish "./TopicSearch/TopicSearch.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENTRYPOINT ["dotnet", "TopicSearch.dll", "--environment=Development"]