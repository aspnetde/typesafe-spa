module Home

open AppNavigation
open Feliz

let render = React.functionComponent(fun () ->
    Html.a [
        prop.text "Hello, stranger! You're not signed in."
        prop.href (AnonymousUrl.Login.Format())
    ]
)
