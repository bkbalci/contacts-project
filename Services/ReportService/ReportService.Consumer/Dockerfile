FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/ReportService/ReportService.Consumer/ReportService.Consumer.csproj", "Services/ReportService/ReportService.Consumer/"]
COPY ["Services/ContactService/ContactService.Application/ContactService.Application.csproj", "Services/ContactService/ContactService.Application/"]
COPY ["Services/ContactService/ContactService.Infrastructure/ContactService.Infrastructure.csproj", "Services/ContactService/ContactService.Infrastructure/"]
COPY ["Services/ContactService/ContactService.Domain/ContactService.Domain.csproj", "Services/ContactService/ContactService.Domain/"]
COPY ["Core/ContactProject.Core/ContactProject.Core.csproj", "Core/ContactProject.Core/"]
COPY ["Services/ReportService/ReportService.Application/ReportService.Application.csproj", "Services/ReportService/ReportService.Application/"]
COPY ["Services/ReportService/ReportService.Domain/ReportService.Domain.csproj", "Services/ReportService/ReportService.Domain/"]
RUN dotnet restore "Services/ReportService/ReportService.Consumer/ReportService.Consumer.csproj"
COPY . .
WORKDIR "/src/Services/ReportService/ReportService.Consumer"
RUN dotnet build "ReportService.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Consumer.dll"]
