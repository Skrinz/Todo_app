const express = require("express");
const cors = require("cors");
const app = express();
const routes = require("./routes");

app.use(cors());

const port = 3000;

app.use(express.json());

app.use("/todos", routes.todosRoutes);
app.use("/users", routes.userRoutes);

app.listen(port, '0.0.0.0', () => {
  console.log(`Server is running on port ${port}`);
});
