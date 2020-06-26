module App

open AppNavigation
open Feliz
open Feliz.Router

let render = React.functionComponent(fun () -> 
    let segments, setSegments = React.useState([])
    
    let application =
        Html.div [
            Html.h1 [ prop.text "Feliz Function Component Test" ]
            match segments |> PageSection.parse with
            | PageSection.Authenticated url -> Authenticated.render { Url = url }
            | PageSection.Anonymous url -> Anonymous.render { Url = url }
        ]

    let router =
        Router.router [
            Router.onUrlChanged(fun segments -> setSegments(segments))
            Router.application [ application ]
        ]

    let session, setSession = React.useState(AppContext.defaultSession)
    let appContextData: AppContext.ContextData =
        { Session = session
          SetSession = setSession }

    React.contextProvider(
        AppContext.instance, 
        appContextData, 
        React.fragment [ router ]
    )
)
