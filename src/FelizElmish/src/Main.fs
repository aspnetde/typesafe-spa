module Main

open Fable.Core.JsInterop

importAll "../styles/main.scss"

open Elmish
open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram App.init App.update App.render
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReactSynchronous "feliz-app"
|> Program.run