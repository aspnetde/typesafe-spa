module EditProfile

open AppNavigation
open Feliz
open Feliz.UseElmish
open Elmish

type State = 
    { UserName: string }

type Msg =
    | UserNameSet of string
    | Save of AppContext.ContextData

let init (session: AppContext.Session) = 
    { UserName = session.User }, Cmd.none

let update msg state =
    match msg with 
    | UserNameSet userName -> { state with UserName = userName }, Cmd.none
    | Save context ->
        context.SetSession({ context.Session with User = state.UserName })
        state, AuthenticatedUrl.Profile.Navigate()

let render = React.functionComponent(fun () ->
    let context = React.useContext AppContext.instance
    let state, dispatch = React.useElmish((fun () -> init(context.Session)), update, [| |])

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
            prop.onClick (fun _ -> dispatch (Save context))
        ]
    ]
)
