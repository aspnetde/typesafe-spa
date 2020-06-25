module Main

open Fable.Core.JsInterop

importAll "../styles/main.scss"

open Elmish
open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkSimple id id (fun _ _ -> App.app())
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReactSynchronous "feliz-app"
|> Program.run