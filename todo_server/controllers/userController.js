const userService = require("../services/userService");

const registerController = async (req, res) => {
  try {
    const { fname, lname, email, password } = req.body;

    //attempt to register user
    const user = await userService.registerUser(fname, lname, email, password);

    //if registration is successful
    res.status(201).json({user:{
      id: user.id,
      fname: user.fname,
      lname: user.lname,
      email: user.email,
      createdAt: user.createdAt,
      updatedAt: user.updatedAt,
    }});

  } catch (error) {

    //if email already exists
    if (error.message === "Email already exists") {
      return res.status(409).json({ message: error.message });
    }

    //unknown error
    console.error(error);
    res.status(500).json({ message: "Something went wrong during registration" });
  }
};

const loginController = async (req, res) => {
  try {
    const { email, password } = req.body;

    //attempt to login user
    const user = await userService.loginUser(email, password);

    //if login is successful
    res.status(200).json({user:{
      id: user.id,
      fname: user.fname,
      lname: user.lname,
      email: user.email,
      createdAt: user.createdAt,
      updatedAt: user.updatedAt,
    }});

  } catch (error) {
    
    //if user is not found or password does not match
    if (error.message === "User not found") {
      return res.status(404).json({ message: error.message });
    }else if (error.message === "Password does not match") {
      return res.status(401).json({ message: error.message });
    }

    //unknown error
    console.error(error);
    res.status(500).json({ message: "Something went wrong during login" });
  }
};

module.exports = {
  registerController,
  loginController,
};
