namespace Navigation

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
type AuthenticatedUrl = AuthenticatedUrl

[<RequireQualifiedAccess>]
type PageSection =
    | Authenticated of AnonymousUrl
    | Anonymous of AnonymousUrl

module AnonymousUrl = 
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> AnonymousUrl.Login
            | _ -> AnonymousUrl.Home
        | None -> AnonymousUrl.Home

module PageSection =
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> PageSection.Anonymous(segments |> AnonymousUrl.parse)
            | _ -> failwith "Not implemented"
        | None -> PageSection.Anonymous(segments |> AnonymousUrl.parse)
