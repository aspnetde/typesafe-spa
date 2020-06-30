import React from "react";

export default function Login() {
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
        }}
      >
        Foo
      </div>
      <input type="text" placeholder="User name" />
      <br />
      <input type="password" placeholder="Password" />
      <br />
      <button>Get in</button>
    </fieldset>
  );
}
