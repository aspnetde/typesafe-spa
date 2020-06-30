import React from "react";
import { Link } from "react-router-dom";
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
      <Link to="/app/profile">Profile</Link>|{" "}
      <a href="/" onClick={logout}>
        Logout
      </a>
      <hr />
      <Dashboard />
      <Profile />
      <EditProfile />
    </div>
  );
}
