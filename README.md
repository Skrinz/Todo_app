# **🚀 Todo App – Server Setup**  

## **🛠️ Getting Started**  
Follow these steps to set up and run the server in **VS Code**:  

```sh
cd todo_server
npm install
```
---

## **🛡️ Server Device Configuration**
 - On the device running the server:
    - Turn off Windows Defender
    - Or atleast whitelist you app and necessary ports
    
---

### **🔄 Apply Database Migrations**  
If you modify the Prisma schema, run:  
```sh
npx prisma migrate dev --name <migration_name>
```

---

## **📦 Installed Dependencies**  
Ensure the following packages are installed:  
```sh
npm install express bcrypt express-validator cors
```

---

## **💡 Recommended VS Code Extensions**  
To enhance development, install:  
- **Prisma** – ORM support  
- **SQLite** – SQLite database management  
- **SQLite Viewer** – View database contents  
- **DevDB** – Database management tool  

---

## **📌 API Endpoints**  

### **📝 Todos**  

#### ** Create a Todo**  
**Endpoint:** `POST URL/todos/`  
**Request Body:**  
```json
{
    "title": "Test",
    "details": "Test details",
    "userId": 2
}
```
**Response:** Returns the created todo task.  

#### **Get All Todos**  
**Endpoint:** `GET URL/todos?userId={userId}`  
**Response:** Returns all todo tasks associated with the specified `userId`.  

#### **Update a Todo**  
**Endpoint:** `PATCH URL/todos/{todoId}`  
**Request Body:** *(Include only fields that need updating)*  
```json
{
    "title": "Updated Title",
    "details": "Updated Details",
    "completed": true
}
```
**Response:** Returns the updated todo task.  

#### **Delete a Todo**  
**Endpoint:** `DELETE URL/todos/{todoId}`  
**Response:** Returns Deleted specified todo task.  

---

### **👤 Users**  

#### **Register a User**  
**Endpoint:** `POST URL/users/register`  
**Request Body:**  
```json
{
    "fname": "firstname",
    "lname": "lastname",
    "email": "test@example.com",
    "password": "testpassword"
}
```
**Response:** Returns the newly created user.  

#### **Login a User**  
**Endpoint:** `POST URL/users/login`  
**Request Body:**  
```json
{
    "email": "test@example.com",
    "password": "testpassword"
}
```
**Response:** Returns user details upon successful login.

---

# **📱 Todo App – Client Setup**

## **🔧 Prerequisites**

Before you begin, make sure the following are installed:

- ✅ **Android SDK**  
- ✅ **Java SDK 17**  
- ✅ **MSVC (Microsoft Visual C++)**  
  > Install via **Visual Studio Build Tools**.

---

## **📲 Device Setup**

To run the app on a physical Android device:

1. **Enable Developer Mode** on your phone  
2. **Enable USB Debugging** in Developer Options  
3. **Connect your phone via USB**

---

## **🚦 Pre-Run Checklist**

Run this to check installed .NET workloads:

```sh
dotnet workload list
```
You should see:
```sh
- maui
- maui-tizen
- maui-android
- android
```
if something's missing:
```sh
dotnet workload install maui
# Repeat for any other missing workloads
```
Clean and restore the project:
```sh
dotnet clean
dotnet restore
```