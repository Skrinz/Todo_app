const { PrismaClient } = require("@prisma/client");
const bcrypt = require("bcrypt");

const prisma = new PrismaClient();

//register user
const registerUser = async (username, email, password) => {
  //check if email already exists
  const existingUser = await prisma.user.findUnique({
    where: {
      email,
    },
  });
  if (existingUser) {
    throw new Error("Email already exists");
  }

  //hash password
  const hashedPassword = await bcrypt.hash(password, 10);

  //create user
  const newUser = await prisma.user.create({
    data: {
      name: username,
      email: email,
      password: hashedPassword,
    },
  });

  return newUser;
};

//login user
const loginUser = async (email, password) => {

  const user = await prisma.user.findUnique({
      where: {
        email
      }
  })

  //If user is not found
  if(!user){
    throw new Error("User not found");
  } 

  //Compare the provided password with the hashed password stored in the database
  const passwordMatch = await bcrypt.compare(password, user.password);

  //Password does not match
  if (!passwordMatch){
    throw new Error("Password does not match");
  }

  //Return the user if the password matches
  return user;
};

module.exports = {
  registerUser,
  loginUser,
};
