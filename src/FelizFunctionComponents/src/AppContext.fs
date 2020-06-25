module AppContext

open Feliz

type Session =
    { User: string }

[<Literal>]
let Name = "Session"

let instance = React.createContext(name = Name, defaultValue = { User = "" })
