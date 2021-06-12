#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HW.MicroService.AuthenticationCenter/HW.MicroService.AuthenticationCenter.csproj", "HW.MicroService.AuthenticationCenter/"]
RUN dotnet restore "HW.MicroService.AuthenticationCenter/HW.MicroService.AuthenticationCenter.csproj"
COPY . .
WORKDIR "/src/HW.MicroService.AuthenticationCenter"
RUN dotnet build "HW.MicroService.AuthenticationCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HW.MicroService.AuthenticationCenter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HW.MicroService.AuthenticationCenter.dll"]