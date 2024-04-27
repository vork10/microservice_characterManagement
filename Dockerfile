# Use the official Microsoft .NET Core SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution file and restore any dependencies (via NuGet)
COPY LocalApi.sln ./
COPY *.csproj ./
COPY */*.csproj ./
RUN dotnet restore LocalApi.sln

# Copy the project files and build our release
COPY . ./
RUN dotnet publish LocalApi.csproj -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "LocalApi.dll"]