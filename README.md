# **Todo App – Server Setup**

## **Setting Up the Server in VS Code**  
Run the following commands to set up the server:  
```sh
cd todo_server
npm install
```

## **Installed Packages**  
These packages are required for the project:  
```sh
npm install express bcrypt express-validator cors
```

## **Setting Up Prisma**  

### **Install Prisma (for development only)**  
```sh
npm install prisma --save-dev
```

### **Apply Database Migrations**  
If you modify the Prisma schema, run:  
```sh
npx prisma migrate dev --name <migration_name>
```

## **Recommended VS Code Extensions**  
To enhance development, install the following extensions:  
- **Prisma** – For Prisma ORM support
- **SQLite** – To work with SQLite databases
- **SQLite Viewer** – For viewing database content
- **DevDB** – Database management tool