module Login

[<Literal>]
let Url = "Login"

open Feliz
open Elmish

type State = 
    { UserName: string
      Password: string
      Error: string option }

type Msg =
    | UserNameSet of string
    | PasswordSet of string
    | StartLogin
    | LoginFailed of string
    | LoginSucceeded

type Intent =
    | NoOp
    | StartSession of string

let init() = { UserName = ""; Password = ""; Error = None }, Cmd.none

let update msg state =
    match msg with
    | UserNameSet userName -> { state with UserName = userName }, NoOp, Cmd.none
    | PasswordSet password -> { state with Password = password }, NoOp, Cmd.none
    | StartLogin -> 
        if state.UserName = "user" && state.Password = "test" then
            state, NoOp, Cmd.ofMsg LoginSucceeded
        else
            state, NoOp, Cmd.ofMsg (LoginFailed "Dude, user name or password are incorrect.")
    | LoginFailed error ->
        { state with Error = Some(error) }, NoOp, Cmd.none
    | LoginSucceeded ->
        state, (StartSession(state.UserName)), Cmd.none

let render state dispatch =
    Html.div [
        Html.fieldSet [
            prop.children [
                Html.legend [ prop.text "Login" ]

                Html.div [
                    prop.text state.Error.Value
                    prop.hidden state.Error.IsNone
                    prop.style [
                        style.backgroundColor.red
                        style.color.white
                        style.fontWeight.bold
                        style.padding 10
                        style.marginBottom 15
                    ]
                ]

                Html.input [
                    prop.type'.text
                    prop.placeholder "User name"
                    prop.onChange (UserNameSet >> dispatch)
                ]
                Html.br []
                Html.input [
                    prop.type'.password
                    prop.placeholder "Password"
                    prop.onChange (PasswordSet >> dispatch)
                ]
                Html.br []
                Html.button [
                    prop.onClick (fun _ -> dispatch StartLogin)
                    prop.text "Get in"
                ]
            ]
        ]
    ]
