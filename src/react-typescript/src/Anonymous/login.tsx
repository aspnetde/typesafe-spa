import React from "react";
import { useHistory } from "react-router-dom";
import AppContext from "../AppContext";

export default function Login() {
  const [userName, setUserName] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [showError, setShowError] = React.useState(false);

  let history = useHistory();
  const appContext = React.useContext(AppContext.instance);

  const isValid = () => userName === "user" && password === "test";

  function handleLogin() {
    if (!isValid()) {
      setShowError(true);
      return;
    }

    setShowError(false);
    appContext.setSession({ ...appContext.session, user: userName });
    history.push("/app/dashboard");
  }

  return (
    <fieldset>
      <legend>Login</legend>
      <div
        style={{
          backgroundColor: "red",
          color: "white",
          fontWeight: "bold",
          padding: 10,
          marginBottom: 15,
          display: showError ? "block" : "none",
        }}
      >
        Oops, user name or password are incorrect.
      </div>
      <input
        type="text"
        placeholder="User name"
        onChange={(e) => setUserName(e.target.value)}
      />
      <br />
      <input
        type="password"
        placeholder="Password"
        onChange={(e) => setPassword(e.target.value)}
      />
      <br />
      <button onClick={handleLogin}>Get in</button>
    </fieldset>
  );
}
