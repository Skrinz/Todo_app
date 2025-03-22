const todosService = require("../services/todosService");   

const createtodoController = async (req, res) => {
    try {
      const todoData = req.body;
  
      //create todo
      const newTodo = await todosService.createTodos(todoData);
  
      res.status(201).json(newTodo);
    } catch (error) {

        console.error("Error creating todo:", error.message);
        res.status(500).json({ error: "Failed to create todo. Please try again later." });
    }
};

//get all todos
const gettodosController = async (req, res) => {
    try {
      const { userId } = req.query; 

      if (!userId) {
        return res.status(400).json({ error: "User ID is required." });
      }
      const todos = await todosService.getTodos(parseInt(userId, 10));
  
      res.status(200).json(todos);
    } catch (error) {
      console.error("Error getting todos:", error.message);
      res.status(500).json({ error: "Failed to get todos. Please try again later." });
    }
}

const patchtodoController = async (req, res) => {
    try {
      const { id } = req.params;
      const todoData = req.body;
  
      const updatedTodo = await todosService.patchTodo(parseInt(id, 10), todoData);
  
      res.status(200).json(updatedTodo);
    } catch (error) {

      //if todo not found
      if (error.message === "Todo not found") {
        return res.status(404).json({ error: error.message });
      }

      //unknown error
      console.error("Error updating todo:", error.message);
      res.status(500).json({ error: "Failed to update todo. Please try again later." });
    }
};

const deletetodoController = async (req, res) => {
    try {
      const { id } = req.params;
  
      //delete todo
      const deletedTodo = await todosService.deleteTodo(parseInt(id, 10));
  
      res.status(200).json(deletedTodo);
    } catch (error) {
  
      //if todo not found
      if (error.message === "Todo not found") {
        return res.status(404).json({ error: error.message });
      }
  
      //unknown error
      console.error("Error deleting todo:", error.message);
      res.status(500).json({ error: "Failed to delete todo. Please try again later." });
    }
  }


module.exports = {
    createtodoController,
    gettodosController,
    patchtodoController,
    deletetodoController,
};