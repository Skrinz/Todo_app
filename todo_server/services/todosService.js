const {PrismaClient} = require('@prisma/client');

const prisma = new PrismaClient();

//create todo
const createTodos = async (todoData) => {
    const newTodo = await prisma.todo.create({
        data: {
            title: todoData.title,
            details: todoData.details,
            completed: false,
            userId: todoData.userId,
        },
    });

    return newTodo;
};

//get all todos
const getTodos = async (userId) => {
    const todos = await prisma.todo.findMany({
        where: { userId },
    });

    return todos;
}

const patchTodo = async (id, todoData) => {
    //check if todo exists
    const existingTodo = await findTodo(id)
    if (!existingTodo) {
        throw new Error("Todo not found");
    }

    //update todo
    const updatedTodo = await prisma.todo.update({
        where: { id },
        data: todoData,
    });

    return updatedTodo;
};

const deleteTodo = async (id) => {
    //check if todo exists
    const existingTodo = await findTodo(id);

    if (!existingTodo) {
        throw new Error("Todo not found");
    }

    //delete todo
    const deletedTodo = await prisma.todo.delete({
        where: { id },
    });

    return deletedTodo;

};

const findTodo = async (id) => {
  const todo = await prisma.todo.findUnique({
    where: { id },
  });
  return todo;
}

module.exports = {
  createTodos,
  getTodos,
  patchTodo,
  deleteTodo,
};