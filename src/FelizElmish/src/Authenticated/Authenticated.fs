module Authenticated

open Feliz
open Feliz.Router
open Elmish

[<RequireQualifiedAccess>]
type Url =
    | Dashboard
    | Profile
    | EditProfile

[<RequireQualifiedAccess>]
type Page =
    | Profile of Profile.State
    | EditProfile of EditProfile.State
    | Dashboard of Dashboard.State

type State = 
    { CurrentUrl: Url
      CurrentPage: Page
      Session: Session }

type Msg = 
    | ProfileMsg of Profile.Msg
    | EditProfileMsg of EditProfile.Msg
    | UrlChanged of string list
    | Logout

type Intent =
    | NoOp
    | Logout
    | UpdateUserName of string

let parseUrl segments =
    match segments with
    | [ "app"; "profile"; "edit" ] -> Url.EditProfile
    | [ "app"; "profile" ] -> Url.Profile
    | _ -> Url.Dashboard

let init session segments = 
    let url = segments |> parseUrl
    let page, cmd =
        match url with 
        | Url.Dashboard ->
            let state', cmd' = Dashboard.init session
            Page.Dashboard state', cmd'
        | Url.Profile ->
            let state', cmd' = Profile.init session
            Page.Profile state', cmd'
        | Url.EditProfile ->
            let state', cmd' = EditProfile.init session
            Page.EditProfile state', cmd'
    
    { CurrentUrl = url
      CurrentPage = page
      Session = session }, cmd

let update msg state =
    match msg, state.CurrentPage with
    | UrlChanged segments, _ ->
        let oldUrl = state.CurrentUrl
        let newUrl = segments |> parseUrl
        let state, command =
            if oldUrl <> newUrl then
                match newUrl with
                | Url.Profile ->
                    let state', cmd' = Profile.init state.Session
                    { state with CurrentPage = Page.Profile state'}, cmd'
                | Url.EditProfile ->
                    let state', cmd' = EditProfile.init state.Session
                    { state with CurrentPage = Page.EditProfile state'}, cmd'
                | Url.Dashboard ->
                    let state', cmd' = Dashboard.init(state.Session)
                    { state with CurrentPage = Page.Dashboard state'}, cmd'
            else
                state, Cmd.none

        let state = { state with CurrentUrl = newUrl }
        state, NoOp, command

    | Msg.EditProfileMsg msg', Page.EditProfile state' ->
        let state', intent, cmd = EditProfile.update msg' state'
        match intent with
        | EditProfile.Intent.NoOp -> 
            { state with CurrentPage = Page.EditProfile state' }, NoOp, Cmd.map EditProfileMsg cmd
        | EditProfile.Intent.UpdateUserName userName ->
            state, UpdateUserName userName, Cmd.none

    | Msg.Logout, _ ->
        state, Logout, Cmd.none

    | _, _ -> 
        // NOTE: Losing exhaustive pattern matching here!
        failwith "Not implemented."

let render (state: State) (dispatch: Msg -> unit) =
    Html.div [
        prop.children [
            Html.a [
                prop.href (Router.format(Dashboard.Url))
                prop.text "Dashboard"
            ]
            Html.text " | "
            Html.a [
                prop.href (Router.format(Profile.Url))
                prop.text "Profile"
            ]
            Html.text " | "
            Html.a [
                prop.text "Logout"
                prop.href "#"
                prop.onClick (fun _ -> dispatch Msg.Logout)
            ]
            Html.hr []
            match state.CurrentPage with
            | Page.Profile state -> Profile.render state (ProfileMsg >> dispatch)
            | Page.EditProfile state -> EditProfile.render state (EditProfileMsg >> dispatch)
            | Page.Dashboard state -> Dashboard.render state
        ]
    ]
