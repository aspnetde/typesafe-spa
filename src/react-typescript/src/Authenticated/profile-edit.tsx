import React from "react";
import { useHistory } from "react-router-dom";
import AppContext from "../AppContext";

export default function EditProfile() {
  const appContext = React.useContext(AppContext.instance);
  const [userName, setUserName] = React.useState(appContext.session.user);
  let history = useHistory();

  function update() {
    appContext.setSession({ ...appContext.session, user: userName });
    history.push("/app/profile");
  }

  return (
    <div>
      <label htmlFor="userName">User name:</label>
      <br />
      <input
        type="text"
        id="userName"
        defaultValue={appContext.session.user}
        onChange={(e) => setUserName(e.target.value)}
      />
      <br />
      <button onClick={update}>Update</button>
    </div>
  );
}
