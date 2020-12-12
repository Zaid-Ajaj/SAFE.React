module Main

open Fable.Core.JsInterop
open Feliz
open Browser.Dom

// import global styles here
importSideEffects "./styles/global.scss"

ReactDOM.render(
    Components.Counter(),
    document.getElementById("feliz-app")
)