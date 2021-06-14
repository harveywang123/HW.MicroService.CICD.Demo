#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM 192.168.1.102/hw.microservice/aspnet-core5:v1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM 192.168.1.102/hw.microservice/sdk-core5:v1 AS build
WORKDIR /src
COPY ["HW.MicroService.ServiceInstance/HW.MicroService.ServiceInstance.csproj", "HW.MicroService.ServiceInstance/"]
COPY ["HW.MicroService.Models/HW.MicroService.Models.csproj", "HW.MicroService.Models/"]
COPY ["HW.MicroService.Services/HW.MicroService.Services.csproj", "HW.MicroService.Services/"]
COPY ["HW.MicroService.Interfaces/HW.MicroService.Interfaces.csproj", "HW.MicroService.Interfaces/"]
RUN dotnet restore "HW.MicroService.ServiceInstance/HW.MicroService.ServiceInstance.csproj"
COPY . .
WORKDIR "/src/HW.MicroService.ServiceInstance"
RUN dotnet build "HW.MicroService.ServiceInstance.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HW.MicroService.ServiceInstance.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HW.MicroService.ServiceInstance.dll"]