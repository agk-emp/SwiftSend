
import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";

const LoginPage = () => {
  const loginSchema = Yup.object().shape({
    email: Yup.string().email("Invalid email").required("Required"),
    password: Yup.string().min(6, "Too short!").required("Required"),
  });

  const handleSubmit = (values) => {
    console.log("Login values:", values);
  };

  return (
    <div className="container">
      <h1>Login to SwiftSend</h1>
      <Formik
        initialValues={{ email: "", password: "" }}
        validationSchema={loginSchema}
        onSubmit={handleSubmit}
      >
        {() => (
          <Form>
            <div>
              <label>Email</label>
              <Field type="email" name="email" />
              <ErrorMessage name="email" component="div" className="error-message" />
            </div>
            <div>
              <label>Password</label>
              <Field type="password" name="password" />
              <ErrorMessage name="password" component="div" className="error-message" />
            </div>
            <button type="submit">Login</button>
            <p>
              Don't have an account? <Link to="/register">Register here</Link>
            </p>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default LoginPage;
