module Main

open Fable.Core.JsInterop
open Feliz
open Browser.Dom

// import global styles here
importSideEffects "./styles/global.scss"

ReactDOM.render(
    Components.Router(),
    document.getElementById("feliz-app")
)