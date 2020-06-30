import React from "react";
import AppContext from "../AppContext";
import { Link } from "react-router-dom";

export default function Profile() {
  const appContext = React.useContext(AppContext.instance);

  return (
    <div>
      Hello, {appContext.session.user}! Go ahead and{" "}
      <Link to="/app/profile/edit">edit</Link> your name.
    </div>
  );
}
