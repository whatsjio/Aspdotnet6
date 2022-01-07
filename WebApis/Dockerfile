#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApis/WebApis.csproj", "WebApis/"]
COPY ["MiddlewareService/MiddlewareService.csproj", "MiddlewareService/"]
COPY ["OperateService/OperateService.csproj", "OperateService/"]
COPY ["PlatData/PlatData.csproj", "PlatData/"]
COPY ["DateModel/DateModel.csproj", "DateModel/"]
COPY ["Tool/Tool.csproj", "Tool/"]
RUN dotnet restore "WebApis/WebApis.csproj"
COPY . .
WORKDIR "/src/WebApis"
RUN dotnet build "WebApis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApis.dll"]