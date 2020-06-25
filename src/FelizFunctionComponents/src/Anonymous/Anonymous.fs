module Anonymous

open Feliz
open Navigation

type Props =
    { Url: AnonymousUrl }

let render = React.functionComponent(fun props ->
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

        match props.Url with 
        | AnonymousUrl.Login -> Login.render()
        | AnonymousUrl.Home -> Home.render()
    ]
)
