module Anonymous

open Feliz
open Navigation

let render = React.functionComponent(fun url ->
    Html.div [
        Html.a [
            prop.text "Home"
            prop.href (AnonymousUrl.Home.Format())
        ]
        Html.text " | "
        Html.a [
            prop.text "Login"
            prop.href (AnonymousUrl.Login.Format())
        ]
        
        Html.hr []

        match url with 
        | AnonymousUrl.Login -> Login.render()
        | AnonymousUrl.Home -> Home.render()
    ]
)
