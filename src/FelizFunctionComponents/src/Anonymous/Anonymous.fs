module Anonymous

open Feliz
open Navigation

let render = React.functionComponent(fun url ->
    Html.div [
        Html.a [
            prop.text "Home"
            prop.href (Url.Home.Format())
        ]
        Html.text " | "
        Html.a [
            prop.text "Login"
            prop.href (Url.Login.Format())
        ]
        
        Html.hr []

        match url with 
        | Url.Login -> Login.render()
        | Url.Home -> Home.render()
    ]
)
