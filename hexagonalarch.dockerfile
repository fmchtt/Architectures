#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Architectures.HexagonalArch.WebApi/Architectures.HexagonalArch.WebApi.csproj", "Architectures.HexagonalArch.WebApi/"]
COPY ["Architectures.HexagonalArch.Domain/Architectures.HexagonalArch.Domain.csproj", "Architectures.HexagonalArch.Domain/"]
COPY ["Architectures.HexagonalArch.Infra/Architectures.HexagonalArch.Infra.csproj", "Architectures.HexagonalArch.Infra/"]
COPY ["Architectures.HexagonalArch.Application/Architectures.HexagonalArch.Application.csproj", "Architectures.HexagonalArch.Application/"]
RUN dotnet restore "Architectures.HexagonalArch.WebApi/Architectures.HexagonalArch.WebApi.csproj"
COPY . .
WORKDIR "/src/Architectures.HexagonalArch.WebApi"
RUN dotnet build "Architectures.HexagonalArch.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Architectures.HexagonalArch.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Architectures.HexagonalArch.WebApi.dll"]