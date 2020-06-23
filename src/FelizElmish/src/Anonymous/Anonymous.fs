module Anonymous

open Feliz
open Feliz.Router
open Elmish

[<RequireQualifiedAccess>]
type Url =
    | Login

[<RequireQualifiedAccess>]
type Page =
    | Login of Login.State

type State = 
    { CurrentUrl: Url
      CurrentPage: Page }

type Msg = 
    | LoginMsg of Login.Msg

type Intent =
    | NoOp
    | StartSession of string

let init _ = 
    let loginState, loginCmd = Login.init()
    { CurrentUrl = Url.Login
      CurrentPage = Page.Login loginState }, loginCmd

let update msg state =
    match msg, state.CurrentPage with
    | LoginMsg msg', Page.Login state' ->
        let state', intent, cmd = Login.update msg' state'
        match intent with
        | Login.Intent.StartSession userName ->
            state, StartSession userName, Cmd.none
        | Login.Intent.NoOp ->
            { state with CurrentPage = Page.Login state' }, NoOp, Cmd.map LoginMsg cmd

let render (state: State) dispatch =
    Html.div [
        prop.children [
            Html.a [
                prop.href (Router.format(""))
                prop.text "Home"
            ]
            Html.hr []
            match state.CurrentPage with
            | Page.Login state -> Login.render state (LoginMsg >> dispatch)
        ]
    ]
