const express = require("express");
const router = express.Router();
const todos = require("../controllers/todosController");
const {todoValidation, validate} = require("../middlewares/validation");

//Create todo
router.post("/", todoValidation, validate, todos.createtodoController);
//display all todos
router.get("/", todos.gettodosController);
//Update portion of a todo
router.patch("/:id", todoValidation, validate, todos.patchtodoController);
//Delete a todo
router.delete("/:id", todos.deletetodoController);

module.exports = router;