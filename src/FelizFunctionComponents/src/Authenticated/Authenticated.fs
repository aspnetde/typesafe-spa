module Authenticated

open AppNavigation
open Feliz

type Props =
    { Url: AuthenticatedUrl }

let render = React.functionComponent(fun props ->
    match props.Url with
    | AuthenticatedUrl.Dashboard -> Dashboard.render()
)
