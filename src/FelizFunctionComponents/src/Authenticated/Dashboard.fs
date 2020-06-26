module Dashboard

open Feliz

let render = React.functionComponent(fun () ->
    let context = React.useContext AppContext.instance
    Html.textf "Welcome, %s! " context.Session.User
)
