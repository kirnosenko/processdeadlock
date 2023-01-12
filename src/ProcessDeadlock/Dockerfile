FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build
WORKDIR "/build"
COPY ["src/ProcessDeadlock/ProcessDeadlock.csproj", "src/ProcessDeadlock/"]
RUN dotnet restore "src/ProcessDeadlock/ProcessDeadlock.csproj"
COPY . .
WORKDIR "/build/src/ProcessDeadlock"
RUN dotnet publish "ProcessDeadlock.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS deploy
RUN apk add --no-cache git
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProcessDeadlock.dll"]