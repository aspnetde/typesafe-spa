import React from "react";
import { Switch, Route } from "react-router-dom";
import { Link } from "react-router-dom";
import Home from "./home";
import Login from "./login";

export default function Anonymous() {
  return (
    <div>
      <Link to="/">Home</Link> | <Link to="/login">Login</Link>
      <hr />
      <Switch>
        <Route exact path="/">
          <Home />
        </Route>
        <Route exact path="/login">
          <Login />
        </Route>
      </Switch>
    </div>
  );
}
