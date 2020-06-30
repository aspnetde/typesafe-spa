import React from "react";
import { HashRouter, Switch, Route } from "react-router-dom";

import Anonymous from "./Anonymous";
import Authenticated from "./Authenticated";

export default function App() {
  return (
    <HashRouter>
      <h1>React + TypeScript Test</h1>
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
    </HashRouter>
  );
}
