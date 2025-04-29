## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Server Setup](#server-setup)
- [Database Configuration](#database-configuration)
- [API Endpoints](#api-endpoints)
- [Client Setup](#client-setup)
- [Testing](#testing)

## Getting Started

These instructions will help you set up and run the Todo App API on your local machine.

### Prerequisites

* Node.js
* npm
* Visual Studio Code
* Prisma CLI
* Android SDK (for mobile app): Follow this [instructions](https://learn.microsoft.com/en-us/dotnet/android/getting-started/installation/dependencies)
* Java SDK 17 (for mobile app)
* Navigate to settings -> builds, execution, deployment -> android. Setup the android sdk and java sdk locations.

### Server Setup

1. Navigate to server directory:
   ```bash
   cd todo_server
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Required packages:
   ```bash
   npm install express bcrypt express-validator cors
   ```

### Database Configuration

1. If there is changes with database schema, apply database migrations:
   ```bash
   npx prisma migrate dev --name <migration-name>
   ```

### API Endpoints

#### Todos

1. Create Todo
    - Endpoint: `POST /todos/`
    - Request Body:
      ```json
      {
        "title": "Test",
        "details": "Test details",
        "userId": 2
      }
      ```
    - Response: Returns the created todo task.

2. Get All Todos
    - Endpoint: `GET /todos?userId={userId}`
    - Response: Returns all todo tasks associated with the specified userId.

3. Update Todo
    - Endpoint: `PATCH /todos/{todoId}`
    - Request Body:
      ```json
      {
        "title": "Updated Title",
        "details": "Updated Details",
        "completed": true
      }
      ```
    - Response: Returns the updated todo task.

4. Delete Todo
    - Endpoint: `DELETE /todos/{todoId}`
    - Response: Returns the deleted todo task.

#### Users

1. Register User
    - Endpoint: `POST /users/register`
    - Request Body:
      ```json
      {
        "fname": "firstname",
        "lname": "lastname",
        "email": "test@example.com",
        "password": "testpassword"
      }
      ```
    - Response: Returns the newly created user.

2. Login User
    - Endpoint: `POST /users/login`
    - Request Body:
      ```json
      {
        "email": "test@example.com",
        "password": "testpassword"
      }
      ```
    - Response: Returns user details upon successful login.

### Client Setup (Mobile App)

1. Prerequisites:
    - Android SDK
    - Java SDK 17

2. Device Setup:
    - Enable Developer Mode on your phone
    - Enable USB Debugging in Developer Options
    - Connect phone via USB

3. Pre-Run Checklist:
   ```bash
   dotnet workload list
   ```
   Expected output:
   ```
   maui
   maui-tizen
   maui-android
   android
   ```
   If workloads are missing:
   ```bash
   dotnet workload install maui
   ```
   Clean and restore project:
   ```bash
   dotnet clean
   dotnet restore
   ```

## Testing

Sample account for testing:
- Email: daccount@gmail.com
- Password: 12345678

## Server Device Configuration

On the device running the server:
1. Disable Windows Defender, OR
2. Whitelist your app and necessary ports

## Recommended VS Code Extensions

1. Prisma: For ORM support
2. SQLite: For SQLite database management
3. SQLite Viewer: To view database contents
4. DevDB: Database management tool