# Stage 1, Prebuild
#
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build-env
 
WORKDIR /src
 
COPY . .
 
# Restore Dependencies
RUN dotnet restore
 
# Publish
RUN dotnet publish HCFG-BackendService/HCFG-BackendService/HCFGBackendService.csproj --output /out/alpine --configuration Release -r alpine.3.7-x64
 
#
# Stage 2, Build runtime
#
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine
 
# Default AspNetCore directory
WORKDIR /app
 
# Copy from build stage
COPY --from=build-env /out/alpine .
 
EXPOSE 80
 
ENTRYPOINT ["dotnet", "HCFGBackendService.dll"] 
