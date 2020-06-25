module Login

open Feliz

let render = React.functionComponent(fun () ->
    Html.div [
        Html.fieldSet [
            Html.legend [ prop.text "Login" ]

            Html.div [
                //prop.text state.Error.Value
                //prop.hidden state.Error.IsNone
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
                //prop.onChange (UserNameSet >> dispatch)
            ]
            Html.br []
            Html.input [
                prop.type'.password
                prop.placeholder "Password"
                //prop.onChange (PasswordSet >> dispatch)
            ]
            Html.br []
            Html.button [
                //prop.onClick (fun _ -> dispatch StartLogin)
                prop.text "Get in"
            ]
        ]
    ]
)
