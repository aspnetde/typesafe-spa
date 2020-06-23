module Profile

let Url = [| "app"; "profile" |]

open Feliz
open Feliz.Router
open Elmish

type State = 
    { Session: Session }

type Msg = Msg

let init (session: Session) = 
    { Session = session; }, Cmd.none

let update _ state =
    state, Cmd.none

let render state _ =
    Html.div [
        prop.children [
            Html.text (sprintf "Hello, %s! Go ahead and " state.Session.User)
            Html.a [
                prop.text "edit"
                prop.href (Router.format EditProfile.Url)
            ]
            Html.text " your name."
        ]
    ]
