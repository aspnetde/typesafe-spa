module Home

open Feliz
open Navigation

let render = React.functionComponent(fun () ->
    Html.a [
        prop.text "Hello, stranger! You're not signed in."
        prop.href (Url.Login.Format())
    ]
)
