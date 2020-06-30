import React from "react";
import { Switch, Route, Link } from "react-router-dom";
import AppContext from "../AppContext";
import Dashboard from "./dashboard";
import Profile from "./profile";
import EditProfile from "./profile-edit";

export default function Authenticated() {
  const appContext = React.useContext(AppContext.instance);
  const logout = () => {
    appContext.setSession({ ...appContext.session, user: "" });
  };

  return (
    <div>
      <Link to="/app/dashboard">Dashboard</Link> |{" "}
      <Link to="/app/profile">Profile</Link> |{" "}
      <a href="/login" onClick={logout}>
        Logout
      </a>
      <hr />
      <Switch>
        <Route exact path="/app/dashboard">
          <Dashboard />
        </Route>
        <Route exact path="/app/profile">
          <Profile />
        </Route>
        <Route exact path="/app/profile/edit">
          <EditProfile />
        </Route>
      </Switch>
    </div>
  );
}
