namespace AppNavigation

open Feliz.Router

[<RequireQualifiedAccess>]
type AnonymousUrl =
    | Home
    | Login 
    with 
    member this.Format() =
        match this with
        | AnonymousUrl.Home -> Router.format("")
        | AnonymousUrl.Login -> Router.format("login")

[<RequireQualifiedAccess>]
type AuthenticatedUrl = 
    | Dashboard
    | Profile
    | EditProfile
    with 
    member this.Format() =
        match this with
        | Dashboard -> Router.format("app", "dashboard")
        | Profile -> Router.format("app", "profile")
        | EditProfile -> Router.format("app", "profile", "edit")
    member this.Navigate() =
        match this with
        | Dashboard -> Router.navigate("app", "dashboard")
        | Profile -> Router.navigate("app", "profile")
        | EditProfile -> Router.navigate("app", "profile", "edit")

[<RequireQualifiedAccess>]
type PageSection =
    | Authenticated of AuthenticatedUrl
    | Anonymous of AnonymousUrl

module AnonymousUrl = 
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> AnonymousUrl.Login
            | _ -> AnonymousUrl.Home
        | None -> AnonymousUrl.Home

module AuthenticatedUrl = 
    let parse segments =
        match segments  with
        | [ "app"; "dashboard" ] -> AuthenticatedUrl.Dashboard
        | [ "app"; "profile"; "edit" ] -> AuthenticatedUrl.EditProfile
        | [ "app"; "profile" ] -> AuthenticatedUrl.Profile
        | _ -> failwith "Unknown authenticated Url"

module PageSection =
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> PageSection.Anonymous(segments |> AnonymousUrl.parse)
            | "app" -> PageSection.Authenticated(segments |> AuthenticatedUrl.parse)
            | _ -> failwith "Not implemented"
        | None -> PageSection.Anonymous(segments |> AnonymousUrl.parse)
