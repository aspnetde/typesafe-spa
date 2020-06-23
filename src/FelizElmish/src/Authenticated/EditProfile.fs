module EditProfile

let Url = [| "app"; "profile"; "edit" |]

open Feliz
open Elmish

type State = 
    { UserName: string }

type Msg =
    | UserNameSet of string
    | Save

type Intent =
    | UpdateUserName of string
    | NoOp

let init (session: Session) = 
    { UserName = session.User }, Cmd.none

let update msg state =
    match msg with 
    | UserNameSet userName -> { state with UserName = userName }, NoOp, Cmd.none
    | Save -> state, UpdateUserName state.UserName, Cmd.none

let render state dispatch =
    Html.div [
        Html.label [
            prop.text "User name: "
            prop.htmlFor "userNameInput"
        ]
        Html.br []
        Html.input [
            prop.type'.text
            prop.id "userNameInput"
            prop.defaultValue state.UserName
            prop.onChange (UserNameSet >> dispatch)
        ]
        Html.br []
        Html.button [
            prop.text "Update"
            prop.onClick (fun _ -> Save |> dispatch)
        ]
    ]
