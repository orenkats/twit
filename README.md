# Twitter API Project

## Overview

This project is a **.NET 9.0 Web API** that integrates with Twitter's API to fetch recent tweets based on user queries. Additionally, it includes a sample **Math API** for basic arithmetic operations.

The API is containerized using **Docker**, configured with **dependency injection**, follows **best practices for exception handling and logging**, and uses **model validation** to ensure proper request handling.

## Features

- Fetch recent tweets using Twitter API.
- Simple Math API (multiplication endpoint).
- Structured exception handling with middleware.
- Logging of requests and responses.
- Docker support for containerized deployment.
- Uses dependency injection for service management.

---

## Project Structure

```
TwitterApi/
├── Controllers/             # API Controllers (Twitter & Math APIs)
├── DTOs/                    # Data Transfer Objects for request/response models
├── Exceptions/              # Custom exception classes (e.g., TwitterApiException)
├── Extensions/              # Service and pipeline configuration extensions
├── Middlewares/             # Logging middleware
├── Models/                  # Data models for handling API responses
├── Services/                # Service layer for interacting with Twitter API
├── Utils/                   # Utility classes (e.g., ExceptionResponseMapper)
├── Properties/              # Configuration files (launchSettings.json)
├── appsettings.json         # Application configuration
├── docker-compose.yml       # Docker Compose file
├── Dockerfile               # Docker image configuration
├── Program.cs               # Application entry point
├── TwitterApi.csproj        # Project definition file
└── Twit.sln                 # Visual Studio solution file
```

---

## Installation & Setup

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)
- Twitter API credentials (API Key, Secret, Bearer Token)

### Configuration

1. **Clone the repository:**

   ```sh
   git clone https://github.com/orenkats/twit.git
   cd twitter-api
   ```

2. **Replace API credentials in configuration files:**

   - **Update** `docker-compose.yml` with your Twitter API credentials:
     ```yaml
     version: '3.8'
     services:
       twitter-api:
         build: .
         ports:
           - "8081:80"
         environment:
           Twitter__ApiKey: "your_api_key"
           Twitter__ApiSecret: "your_api_secret"
           Twitter__AccessToken: "your_access_token"
           Twitter__AccessTokenSecret: "your_access_token_secret"
     ```
   - **Update** `appsettings.json` with your Twitter API credentials:
     ```json
     {
       "Twitter": {
         "ApiKey": "your_api_key",
         "ApiSecret": "your_api_secret",
         "AccessToken": "your_access_token",
         "AccessTokenSecret": "your_access_token_secret",
         "BearerToken": "your_bearer_token",
         "BaseUrl": "https://api.twitter.com/2"
       },
       "AllowedHosts": "*"
     }
     ```

### Running the Application

#### **Using .NET CLI**

```sh
# Restore dependencies
dotnet restore

# Build the application
dotnet build

# Run the application
dotnet run --project TwitterApi
```

#### **Using Docker**

```sh
# Build and run the container
docker-compose up --build
```

This will expose the API at `http://localhost:8081`.

---

## API Endpoints

### 1. **Twitter API**

#### **Get Tweets**

- **Endpoint:** `GET /api/twitter/tweets`
- **Query Parameters:**
  - `query` (required): Search term (e.g., `news`)
  - `maxResults` (optional): Number of tweets (default: `10`)
- **Response:**
  ```json
  [
    { "text": "Latest tweet content..." },
    { "text": "Another tweet..." }
  ]
  ```

### 2. **Math API**

#### **Multiply Numbers**

- **Endpoint:** `POST /api/math/multiply`
- **Request Body:**
  ```json
  { "num1": 5, "num2": 3 }
  ```
- **Response:**
  ```json
  { "result": 15 }
  ```

---

## Exception Handling & Logging

- **Middleware-based error handling** ensures structured responses for API failures.
- **Custom exceptions** like `TwitterApiException` provide detailed error messages.
- **Request-response logging middleware** logs each request and response.

---

## Deployment

### **Docker Deployment**

```sh
docker build -t twitter-api .
docker run -p 8081:80 twitter-api
```

### **Kubernetes Deployment (Optional)**

1. Create a deployment file `deployment.yaml`:
   ```yaml
   apiVersion: apps/v1
   kind: Deployment
   metadata:
     name: twitter-api
   spec:
     replicas: 2
     selector:
       matchLabels:
         app: twitter-api
     template:
       metadata:
         labels:
           app: twitter-api
       spec:
         containers:
           - name: twitter-api
             image: your-dockerhub/twitter-api:latest
             ports:
               - containerPort: 80
   ```
2. Apply the deployment:
   ```sh
   kubectl apply -f deployment.yaml
   ```

---

## Contributing

1. Fork the repository.
2. Create a new feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Add new feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a pull request.

---

## License

This project is licensed under the **MIT License**. Feel free to use and modify it.

