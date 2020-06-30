import React from "react";
import { Link } from "react-router-dom";
import Dashboard from "./dashboard";
import Profile from "./profile";
import EditProfile from "./profile-edit";

export default function Authenticated() {
  return (
    <div>
      <Link to="/app/dashboard">Dashboard</Link> |{" "}
      <Link to="/app/profile">Profile</Link>| <a href="#">Logout</a>
      <hr />
      <Dashboard />
      <Profile />
      <EditProfile />
    </div>
  );
}
