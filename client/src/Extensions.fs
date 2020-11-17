[<AutoOpen>]
module Extensions

open Elmish
open System
open Fable.Core
open Fable.Core.JsInterop

let isDevelopment =
    #if DEBUG
    true
    #else
    false
    #endif

module Log =
    /// Logs error to the console during development
    let developmentError (error: exn) =
        if isDevelopment
        then Browser.Dom.console.error(error)

module Cmd =
    /// Converts an asynchronous operation that returns a message into into a command of that message.
    /// Logs unexpected errors to the console while in development mode.
    let fromAsync (operation: Async<'msg>) : Cmd<'msg> =
        let delayedCmd (dispatch: 'msg -> unit) : unit =
            let delayedDispatch = async {
                match! Async.Catch operation with
                | Choice1Of2 msg -> dispatch msg
                | Choice2Of2 error -> Log.developmentError error
            }

            Async.StartImmediate delayedDispatch

        Cmd.ofSub delayedCmd

[<RequireQualifiedAccess>]
module StaticFile =

    /// Function that imports a static file by it's relative path.
    let inline import (path: string) : string = importDefault<string> path

[<RequireQualifiedAccess>]
module Config =
    /// Returns the value of a configured variable using its key.
    /// Retursn empty string when the value does not exist
    [<Emit("process.env[$0] ? process.env[$0] : ''")>]
    let variable (key: string) : string = jsNative

    /// Tries to find the value of the configured variable if it is defined or returns a given default value otherwise.
    let variableOrDefault (key: string) (defaultValue: string) =
        let foundValue = variable key
        if String.IsNullOrWhiteSpace foundValue
        then defaultValue
        else foundValue

// Stylesheet API
// let stylehsheet = Stylesheet.load "./fancy.css"
// stylesheet.["fancy-class"] which returns a string
module Stylesheet =

    type IStylesheet =
        [<Emit "$0[$1]">]
        abstract Item : className:string -> string

    /// Loads a CSS module and makes the classes within available
    let inline load (path: string) = importDefault<IStylesheet> path