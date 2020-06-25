module App

open Navigation
open Feliz
open Feliz.Router

let render = React.functionComponent(fun () -> 
    let segments, setSegments = React.useState([])
    
    let application =
        Html.div [
            Html.h1 [ prop.text "Feliz Function Component Test" ]
            match segments |> PageSection.parse with
            | PageSection.Authenticated -> Html.div[]
            | PageSection.Anonymous url -> Anonymous.render(url)
        ]

    Router.router [
        Router.onUrlChanged(fun segments -> setSegments(segments))
        Router.application [ application ]
    ]
)
