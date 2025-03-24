# **Todo App – Server Setup**  

## **Getting Started**  
Follow these steps to set up and run the server in **VS Code**:  

```sh
cd todo_server
npm install
```

### **Apply Database Migrations**  
If you modify the Prisma schema, run:  
```sh
npx prisma migrate dev --name <migration_name>
```

---

## **Installed Dependencies**  
Ensure the following packages are installed:  
```sh
npm install express bcrypt express-validator cors
```

---

## **Recommended VS Code Extensions**  
To enhance development, install:  
- **Prisma** – ORM support  
- **SQLite** – SQLite database management  
- **SQLite Viewer** – View database contents  
- **DevDB** – Database management tool  

---

## **API Endpoints**  

### **Todos**  

<font color="red">#### **Create a Todo**</font>  
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

<font color="red">#### **Get All Todos**  </font>  
**Endpoint:** `GET URL/todos?userId={userId}`  
**Response:** Returns all todo tasks associated with the specified `userId`.  

<font color="red">#### **Update a Todo** *(Requires Discussion on Access Control)*  </font>   
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

<font color="red">#### **Delete a Todo**  </font>  
**Endpoint:** `DELETE URL/todos/{todoId}`  
**Response:** Returns deleted specified todo task.  

---

### **Users**  

<font color="red">#### **Register a User**  </font>  
**Endpoint:** `POST URL/users/register`  
**Request Body:**  
```json
{
    "username": "testuser",
    "email": "test@example.com",
    "password": "testpassword"
}
```
**Response:** Returns the newly created user.  

<font color="red"> #### **Login a User**  </font>  
**Endpoint:** `POST URL/users/login`  
**Request Body:**  
```json
{
    "email": "test@example.com",
    "password": "testpassword"
}
```
**Response:** Returns user details upon successful login.

