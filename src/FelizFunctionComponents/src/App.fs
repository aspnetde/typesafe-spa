module App

open Foo
open Feliz
open Feliz.Router

let app = React.functionComponent(fun () -> 
    let segments, setSegments = React.useState([])
    
    let application =
        Html.div [
            Html.h1 [ prop.text "Feliz Function Component Test" ]
            match segments |> Url.parse with
            | Url.Login -> Html.div[]
            | Url.Home ->
                Html.a [
                    prop.text "Hello, stranger! You're not signed in."
                    prop.href (Url.Login.Format())
                ]
        ]

    Router.router [
        Router.onUrlChanged(fun segments -> setSegments(segments))
        Router.application [ application ]
    ]
)
