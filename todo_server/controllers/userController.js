const userService = require("../services/userService");

const registerController = async (req, res) => {
  try {
    const { username, email, password } = req.body;

    //attempt to register user
    const user = await userService.registerUser(username, email, password);

    //if registration is successful
    res.status(201).json(user);

  } catch (error) {

    //if email already exists
    if (error.message === "Email already exists") {
      return res.status(409).json({ message: error.message });
    }

    //unknown error
    console.error(error);
    res
      .status(500)
      .json({ message: "Something went wrong during registration" });
  }
};

const loginController = async (req, res) => {
  try {
    const { email, password } = req.body;

    //attempt to login user
    const user = await userService.loginUser(email, password);

    //if login is successful
    res.status(200).json(user);

  } catch (error) {
    //unknown error
    console.error(error);
    res.status(500).json({ message: "Something went wrong during login" });
  }
};

module.exports = {
  registerController,
  loginController,
};
