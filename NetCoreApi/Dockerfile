#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src
COPY ["global.json", "."]
COPY ["NetCoreApi/NetCoreApi.csproj", "NetCoreApi/"]
RUN dotnet restore "NetCoreApi/NetCoreApi.csproj"
COPY . .
WORKDIR "/src/NetCoreApi"
RUN dotnet build "NetCoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreApi.dll"]