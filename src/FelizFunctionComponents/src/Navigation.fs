namespace Navigation

open Feliz.Router

[<RequireQualifiedAccess>]
type Url =
    | Home
    | Login 
    with 
    member this.Format() =
        match this with
        | Url.Home -> Router.format("")
        | Url.Login -> Router.format("login")

[<RequireQualifiedAccess>]
type PageSection =
    | Authenticated of Url
    | Anonymous of Url

module Url = 
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> Url.Login
            | _ -> Url.Home
        | None -> Url.Home

module PageSection =
    let parse segments =
        let section =
            match segments |> List.tryHead with
            | Some head ->
                match head with
                | "login" -> PageSection.Anonymous
                | _ -> failwith "Not implemented"
            | None -> PageSection.Anonymous

        let url = segments |> Url.parse
        section url
