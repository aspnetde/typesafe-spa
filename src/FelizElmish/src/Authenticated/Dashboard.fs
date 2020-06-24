module Dashboard

let Url = [| "app"; "dashboard" |]

open Feliz
open Elmish

type State = { Session: Session }

let init (session: Session) = 
    { Session = session }, Cmd.none

let render state =
    Html.div [
        prop.textf "Welcome, %s! " state.Session.User
    ]
