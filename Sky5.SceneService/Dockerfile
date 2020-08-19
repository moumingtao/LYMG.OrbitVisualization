#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sky5.SceneService/Sky5.SceneService.csproj", "Sky5.SceneService/"]
RUN dotnet restore "Sky5.SceneService/Sky5.SceneService.csproj"
COPY . .
WORKDIR "/src/Sky5.SceneService"
RUN dotnet build "Sky5.SceneService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sky5.SceneService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sky5.SceneService.dll"]