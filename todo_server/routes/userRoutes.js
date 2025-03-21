const express = require("express");
const router = express.Router();
const user = require("../controllers/userController");
const {
  registerValidation,
  validate,
  loginValidation,
} = require("../middlewares/validation");

router.post("/register", registerValidation, validate, user.registerController);
router.post("/login", loginValidation, validate, user.loginController);

module.exports = router; 
