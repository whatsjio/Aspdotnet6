#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5025
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AlpathAny/AlpathAny.csproj", "AlpathAny/"]
COPY ["MiddlewareService/MiddlewareService.csproj", "MiddlewareService/"]
COPY ["OperateService/OperateService.csproj", "OperateService/"]
COPY ["PlatData/PlatData.csproj", "PlatData/"]
COPY ["DateModel/DateModel.csproj", "DateModel/"]
COPY ["Tool/Tool.csproj", "Tool/"]
RUN dotnet restore "AlpathAny/AlpathAny.csproj"
COPY . .
WORKDIR "/src/AlpathAny"
RUN dotnet build "AlpathAny.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlpathAny.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlpathAny.dll"]