import React from "react";
import { Link } from "react-router-dom";
import Home from "./home";
import Login from "./login";

export default function Anonymous() {
  return (
    <div>
      <Link to="/">Home</Link> | <Link to="/Login">Login</Link>
      <hr />
      <Login />
      <Home />
    </div>
  );
}
