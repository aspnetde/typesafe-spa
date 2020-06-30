import React from "react";
import AppContext from "../AppContext";

export default function Dashboard() {
  const appContext = React.useContext(AppContext.instance);
  return <div>Welcome {appContext.session.user}!</div>;
}
