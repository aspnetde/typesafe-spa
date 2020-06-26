module Profile

open AppNavigation
open Feliz

let render = React.functionComponent(fun () ->
    let context = React.useContext AppContext.instance
    Html.div [
        Html.textf "Hello, %s! Go ahead and " context.Session.User
        Html.a [
            prop.text "edit"
            prop.href (AuthenticatedUrl.EditProfile.Format())
        ]
        Html.text " your name."
    ]
)
