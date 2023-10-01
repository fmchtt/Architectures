#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Architectures.CleanArch.WebApi/Architectures.CleanArch.WebApi.csproj", "Architectures.CleanArch.WebApi/"]
COPY ["Architectures.CleanArch.Domain/Architectures.CleanArch.Domain.csproj", "Architectures.CleanArch.Domain/"]
COPY ["Architectures.CleanArch.Infra/Architectures.CleanArch.Infra.csproj", "Architectures.CleanArch.Infra/"]
COPY ["Architectures.CleanArch.Application/Architectures.CleanArch.Application.csproj", "Architectures.CleanArch.Application/"]
RUN dotnet restore "Architectures.CleanArch.WebApi/Architectures.CleanArch.WebApi.csproj"
COPY . .
WORKDIR "/src/Architectures.CleanArch.WebApi"
RUN dotnet build "Architectures.CleanArch.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Architectures.CleanArch.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Architectures.CleanArch.WebApi.dll"]