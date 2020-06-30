import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import AppContext from "./AppContext";
import Anonymous from "./Anonymous";
import Authenticated from "./Authenticated";

export default function App() {
  const [session, setSession] = React.useState(AppContext.defaultSession);
  return (
    <Router>
      <h1>React + TypeScript Test</h1>
      <AppContext.instance.Provider
        value={{ session: session, setSession: setSession }}
      >
        <Switch>
          <Route exact path="/">
            <Anonymous />
          </Route>
          <Route exact path="/login">
            <Anonymous />
          </Route>
          <Route path="/app">
            <Authenticated />
          </Route>
        </Switch>
      </AppContext.instance.Provider>
    </Router>
  );
}
