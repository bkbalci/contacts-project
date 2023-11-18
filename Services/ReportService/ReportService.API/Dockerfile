FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/ReportService/ReportService.API/ReportService.API.csproj", "Services/ReportService/ReportService.API/"]
COPY ["Services/ReportService/ReportService.Application/ReportService.Application.csproj", "Services/ReportService/ReportService.Application/"]
COPY ["Services/ReportService/ReportService.Domain/ReportService.Domain.csproj", "Services/ReportService/ReportService.Domain/"]
COPY ["Core/ContactProject.Core/ContactProject.Core.csproj", "Core/ContactProject.Core/"]
RUN dotnet restore "Services/ReportService/ReportService.API/ReportService.API.csproj"
COPY . .
WORKDIR "/src/Services/ReportService/ReportService.API"
RUN dotnet build "ReportService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.API.dll"]
