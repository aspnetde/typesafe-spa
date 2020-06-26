module Login

open AppNavigation
open Elmish
open Feliz
open Feliz.Router
open Feliz.UseElmish

type private State = 
    { UserName: string
      Password: string
      Error: string option }

type private Msg =
    | UserNameSet of string
    | PasswordSet of string
    | StartLogin of AppContext.ContextData
    | LoginFailed of string
    | LoginCompleted

let private init() = { UserName = ""; Password = ""; Error = None }, Cmd.none

let private updateSession (context: AppContext.ContextData) user =
    context.SetSession({ context.Session with User = user })
    LoginCompleted

let private update msg state =
    match msg with
    | UserNameSet userName -> { state with UserName = userName }, Cmd.none
    | PasswordSet password -> { state with Password = password }, Cmd.none
    | StartLogin context -> 
        if state.UserName = "user" && state.Password = "test" then
            state, Cmd.ofMsg (updateSession context state.UserName)
        else
            state, Cmd.ofMsg (LoginFailed "Oops, user name or password are incorrect.")
    | LoginFailed error ->
        { state with Error = Some(error) }, Cmd.none
    | LoginCompleted ->
        state, AuthenticatedUrl.Dashboard.Navigate()

let render = React.functionComponent(fun () ->
    let state, dispatch = React.useElmish(init, update, [| |])
    let context = React.useContext AppContext.instance

    Html.div [
        Html.fieldSet [
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
                prop.onClick (fun _ -> dispatch (StartLogin context))
                prop.text "Get in"
            ]
        ]
    ]
)
