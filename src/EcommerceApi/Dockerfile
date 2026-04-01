# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copy csproj files and restore
COPY src/EcommerceApi/EcommerceApi.csproj src/EcommerceApi/
COPY src/EcommerceApplication/EcommerceApplication.csproj src/EcommerceApplication/
COPY src/EcommerceInfrastructure/EcommerceInfrastructure.csproj src/EcommerceInfrastructure/
COPY src/EcommerceDomain/EcommerceDomain.csproj src/EcommerceDomain/

RUN dotnet restore "src/EcommerceApi/EcommerceApi.csproj"

# 2. Copy everything else and publish
COPY . .
WORKDIR "/app/src/EcommerceApi"
RUN dotnet publish -c Release -o /app/publish --no-restore

# STAGE 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 3. Create the directory for the volume mount
# This must happen in the RUNTIME stage
RUN mkdir -p /app/wwwroot/uploads/products 

# 4. Copy the published files from the build stage
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "EcommerceApi.dll"]