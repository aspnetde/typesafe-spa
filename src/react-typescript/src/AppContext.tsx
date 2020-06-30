import React from "react";

interface Session {
  user: string;
}

interface ContextData {
  session: Session;
  setSession: (session: Session) => void;
}

const defaultSession = { user: "" };

const defaultContext: ContextData = {
  session: defaultSession,
  setSession: function (session) {},
};

export default {
  defaultSession: defaultSession,
  instance: React.createContext<ContextData>(defaultContext),
};
