#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000-5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RouletteLR/RouletteLR.csproj", "RouletteLR/"]
RUN dotnet restore "RouletteLR/RouletteLR.csproj"
COPY . .
WORKDIR "/src/RouletteLR"
RUN dotnet build "RouletteLR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RouletteLR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "RouletteLR.dll","watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]

docker-compose -f run.yml up --build

