module Authenticated

open AppNavigation
open Feliz

type Props =
    { Url: AuthenticatedUrl }

let render = React.functionComponent(fun props ->
    let context = React.useContext AppContext.instance
    let logout _ =
        context.SetSession({ context.Session with User = ""})
        Browser.Dom.window.location.href <- AnonymousUrl.Home.Format()

    Html.div [
        Html.a [
            prop.href (AuthenticatedUrl.Dashboard.Format())
            prop.text "Dashboard"
        ]
        Html.text " | "
        Html.a [
            //prop.href (Router.format(Profile.Url))
            prop.text "Profile"
        ]
        Html.text " | "
        Html.a [
            prop.text "Logout"
            prop.href "#"
            prop.onClick logout
        ]
        Html.hr []
        match props.Url with
        | AuthenticatedUrl.Dashboard -> Dashboard.render()
    ]
)
