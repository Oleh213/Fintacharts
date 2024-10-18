# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the solution file
COPY Fintacharts.sln ./

# Copy all project files to their respective directories
COPY Fintacharts/Fintacharts.csproj ./Fintacharts/
COPY Fintacharts.Abstractions/Fintacharts.Abstractions.csproj ./Fintacharts.Abstractions/
COPY Fintacharts.Business/Fintacharts.Business.csproj ./Fintacharts.Business/
COPY Fintacharts.DataService/Fintacharts.DataService.csproj ./Fintacharts.DataService/
COPY Fintacharts.Infrastructure/Fintacharts.Infrastructure.csproj ./Fintacharts.Infrastructure/

# Restore the project dependencies
RUN dotnet restore Fintacharts.sln  # Restore from the solution file

# Copy the rest of the code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Fintacharts.dll"]
