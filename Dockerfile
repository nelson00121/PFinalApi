# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY Nelson.sln ./

# Copy project files
COPY Api/Api.csproj Api/
COPY Database/Database.csproj Database/
COPY BackOffice/BackOffice.csproj BackOffice/
COPY Bucket/Bucket.csproj Bucket/
COPY Bycript/Bycript.csproj Bycript/

# Restore dependencies
RUN dotnet restore Nelson.sln

# Copy source code
COPY . .

# Build and publish
WORKDIR /src/Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Install MySQL client (if needed for migrations)
RUN apt-get update && apt-get install -y default-mysql-client && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Railway uses PORT environment variable
ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Api.dll"]