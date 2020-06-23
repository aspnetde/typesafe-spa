module App

open Feliz
open Feliz.Router
open Elmish

[<RequireQualifiedAccess>]
type Url =
    | Home
    | Authenticated of string list
    | Anonymous of string list

[<RequireQualifiedAccess>]
type Page =
    | Home
    | Anonymous of Anonymous.State
    | Authenticated of Authenticated.State

type State = 
    { CurrentUrl: Url
      CurrentPage: Page
      Session: Session option }

type Msg = 
    | AuthenticatedMsg of Authenticated.Msg
    | AnonymousMsg of Anonymous.Msg
    | UrlChanged of string list

let init() = 
    { CurrentUrl = Url.Home
      CurrentPage = Page.Home
      Session = None }, Cmd.none

let parseUrl segments =
    match segments |> List.tryHead with
    | Some head ->
        match head with
        | "login" -> Url.Anonymous segments
        | "app" -> Url.Authenticated segments
        | _ -> Url.Home
    | None -> Url.Home

let update msg state =
    match msg, state.CurrentPage with
    | UrlChanged segments, _ ->
        let oldUrl = state.CurrentUrl
        let newUrl = segments |> parseUrl
        let state, command =
            if oldUrl <> newUrl then
                match newUrl with
                | Url.Authenticated ->
                    match state.Session with
                    | Some session ->
                        let state', cmd' = Authenticated.init session segments
                        { state with CurrentPage = Page.Authenticated state'}, cmd'
                    | None ->
                        { state with CurrentPage = Page.Home}, Cmd.none
                | Url.Anonymous ->
                    let state', cmd' = Anonymous.init segments
                    { state with CurrentPage = Page.Anonymous state'}, cmd'
                | Url.Home ->
                    { state with CurrentPage = Page.Home}, Cmd.none
            else
                state, Cmd.none

        let state = { state with CurrentUrl = newUrl }
        state, command

    | AuthenticatedMsg msg', Page.Authenticated state' ->
        let state', intent, cmd = Authenticated.update msg' state'
        match intent with
        | Authenticated.Intent.Logout ->
            { state with Session = None }, Router.navigate("")
        | Authenticated.Intent.UpdateUserName userName ->
            let session = { state.Session.Value with User = userName }
            let state = { state with Session = Some(session) }
            state, Router.navigate(Profile.Url)
        | Authenticated.Intent.NoOp ->
            { state with CurrentPage = Page.Authenticated state' }, Cmd.map AuthenticatedMsg cmd

    | AnonymousMsg msg', Page.Anonymous state' ->
        let state', intent, cmd = Anonymous.update msg' state'
        match intent with
        | Anonymous.Intent.StartSession userName ->
            { state with Session = Some({ User = userName }) }, Router.navigate(Dashboard.Url)
        | Anonymous.Intent.NoOp ->
            { state with CurrentPage = Page.Anonymous state' }, Cmd.map AnonymousMsg cmd

    | _, _ -> 
        // NOTE: Losing exhaustive pattern matching here!
        failwith "Not implemented."

let render state dispatch =
    let application =
        Html.div [
            prop.children [
                Html.h1 [ prop.text "Feliz Full Test" ]
                match state.CurrentPage with
                | Page.Authenticated state -> Authenticated.render state (AuthenticatedMsg >> dispatch)
                | Page.Anonymous state -> Anonymous.render state (AnonymousMsg >> dispatch)
                | Page.Home -> 
                    Html.a [
                        prop.text "Hello, stranger! You're not signed in."
                        prop.href (Router.format(Login.Url))
                    ]
            ]
        ]

    Router.router [
        Router.onUrlChanged (UrlChanged >> dispatch)
        Router.application [ application ]
    ]
