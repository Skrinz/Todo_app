# Todo_app

---

## <strong>Server Setup in vs code<strong>

cd todo_server
npm install

## Specifics packages installed:

npm install express
npm install bcrypt
npm install express-validator
npm install cors

<strong>Setup Prisma</strong>

--Include Prisma only in development and not in production --
npm install prisma --save-dev

--Setting up Prisma for the first time in project--
npx prisma init

--If editing the schema, run this to migrate the schema--
npx prisma migrate dev --name <migration_name>

--------------Extensions-------------------
Prisma
sqlite
sqlite viewer
devdb
