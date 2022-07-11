#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TopicSearch/TopicSearch.csproj", "TopicSearch/"]
RUN dotnet restore "TopicSearch/TopicSearch.csproj"
COPY . .
WORKDIR "/src/TopicSearch"
RUN dotnet build "TopicSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TopicSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT ["dotnet", "TopicSearch.dll"]