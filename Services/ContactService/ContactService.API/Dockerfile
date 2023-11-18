FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/ContactService/ContactService.API/ContactService.API.csproj", "Services/ContactService/ContactService.API/"]
COPY ["Services/ContactService/ContactService.Application/ContactService.Application.csproj", "Services/ContactService/ContactService.Application/"]
COPY ["Services/ContactService/ContactService.Infrastructure/ContactService.Infrastructure.csproj", "Services/ContactService/ContactService.Infrastructure/"]
COPY ["Services/ContactService/ContactService.Domain/ContactService.Domain.csproj", "Services/ContactService/ContactService.Domain/"]
COPY ["Core/ContactProject.Core/ContactProject.Core.csproj", "Core/ContactProject.Core/"]
RUN dotnet restore "Services/ContactService/ContactService.API/ContactService.API.csproj"
COPY . .
WORKDIR "/src/Services/ContactService/ContactService.API"
RUN dotnet build "ContactService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactService.API.dll"]
