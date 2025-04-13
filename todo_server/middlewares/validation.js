const { body, validationResult } = require("express-validator");

const registerValidation = [
  body("fname").notEmpty().withMessage("First name is required"),
  body("lname").notEmpty().withMessage("Last name is required"),
  body("email").isEmail().withMessage("Invalid email format"),
  body("password").isLength({ min: 8 }).withMessage("Password must be at least 8 characters"),
];

const loginValidation = [
  body("email").isEmail().withMessage("Invalid email format"),
  body("password").notEmpty().withMessage("Password is required"),
];

const todoValidation = [
  body("title").notEmpty().withMessage("Title is required"),
];

const validate = (req, res, next) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    const errorDetails = errors.array().map((err) => ({
      field: err.param,
      message: err.msg,
    }));

    return res.status(400).json({
      status: "error",
      message: "Validation failed",
      errors: errorDetails,
    });
  }
  next();
};

module.exports = {
  registerValidation,
  loginValidation,
  todoValidation,
  validate,
};
