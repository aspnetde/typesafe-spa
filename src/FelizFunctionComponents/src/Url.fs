namespace Foo

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

module Url = 
    let parse segments =
        match segments |> List.tryHead with
        | Some head ->
            match head with
            | "login" -> Url.Login
            | _ -> Url.Home
        | None -> Url.Home
