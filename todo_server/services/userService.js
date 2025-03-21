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
    console.error("Email already exists");
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
    try{
        const user = await prisma.user.findUnique({
            where: {
                email
            }
        })
        if(!user) return false; //User not found

        //Compare the provided password with the hashed password stored in the database
        const passwordMatch = await bcrypt.compare(password, user.password);

        if (!passwordMatch) return false; //Password does not match

        //Return the user if the password matches
        return user;

    }catch(error){
        console.error(error);
        return false;
    }
};

module.exports = {
  registerUser,
  loginUser,
};
