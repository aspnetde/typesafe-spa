module AppContext

open Feliz

type Session =
    { User: string }

type ContextData =
    { Session: Session
      SetSession: Session -> unit }

let defaultSession = { User = "" }

let private defaultData =
    { Session = defaultSession
      SetSession = fun _ -> () }

let instance = 
    React.createContext(name = "Session", defaultValue = defaultData)
