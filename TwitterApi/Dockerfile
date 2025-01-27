# Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port
EXPOSE 80
ENTRYPOINT ["dotnet", "TwitterApi.dll"]
