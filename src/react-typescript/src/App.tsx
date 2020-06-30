import React from "react";
import {
  HashRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";

import AppContext from "./AppContext";
import Anonymous from "./Anonymous";
import Authenticated from "./Authenticated";

interface IPrivateRouteProps {
  children: React.ReactNode;
  path: string;
  isAuthenticated: Boolean;
}

function PrivateRoute({
  children,
  isAuthenticated,
  ...rest
}: IPrivateRouteProps) {
  return (
    <Route
      {...rest}
      render={({ location }) =>
        isAuthenticated ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
}

export default function App() {
  const [session, setSession] = React.useState(AppContext.defaultSession);
  const isAuthenticated = session.user?.length > 0;

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
          <PrivateRoute isAuthenticated={isAuthenticated} path="/app">
            <Authenticated />
          </PrivateRoute>
        </Switch>
      </AppContext.instance.Provider>
    </Router>
  );
}
