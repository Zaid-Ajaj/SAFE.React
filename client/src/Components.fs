module Components

open Feliz

// A simple React component

[<ReactComponent>]
let Counter() =
    let (count, setCount) = React.useState(0)
    Html.div [
        prop.style [ style.padding 10 ]
        prop.children [
            Html.h1 count
            Html.button [
                prop.text "Increment"
                prop.onClick (fun _ -> setCount(count + 1))
            ]
        ]
    ]

// Using locally defined stylesheet modules
// make sure the value is 'private'
let private stylesheet = Stylesheet.load "./styles/counter.scss"

// use class names from the stylesheet
[<ReactComponent>]
let StyledCounter() =
    let (count, setCount) = React.useState(0)
    Html.div [
        prop.className stylesheet.["main-container"]
        prop.children [
            Html.h1 count
            Html.button [
                prop.className stylesheet.["increment-button"]
                prop.text "Increment"
                prop.onClick (fun _ -> setCount(count + 1))
            ]
        ]
    ]

// Using imported global Bulma classes from global.scss
[<ReactComponent>]
let BulmaCounter() =
    let (count, setCount) = React.useState(0)
    Html.div [
        Html.h1 [
            prop.className "title"
            prop.text count
        ]

        Html.button [
            prop.className "button"
            prop.text "Increment"
            prop.onClick (fun _ -> setCount(count + 1))
        ]
    ]

open Feliz.UseDeferred

// Calling backend (executing async code)
// Learn more about Feliz.UseDeferred https://zaid-ajaj.github.io/Feliz/#/Hooks/UseDeferred
// Learn about simple async calls with effects https://zaid-ajaj.github.io/Feliz/#/Feliz/React/EffectfulComponents
[<ReactComponent>]
let ServerCounter() =
    let data = React.useDeferred(Server.Api.Counter(), [| |])
    match data with
    | Deferred.HasNotStartedYet -> Html.none
    | Deferred.InProgress -> Html.h1 "Loading"
    | Deferred.Failed error -> Html.h1 error.Message
    | Deferred.Resolved counter -> Html.h1 counter.value

// Simple interop with existing React components
// input parameters are compiled as props for the imported component
// Use the component just like a function: About "Feliz"
[<ReactComponent(import="About", from="./About.jsx")>]
let About (title: string) = Html.none


open Feliz.Router

// Routing in Feliz apps
// Learn about Feliz.Router https://github.com/Zaid-Ajaj/Feliz.Router
[<ReactComponent>]
let Router() =
    let currentUrl, updateCurrentUrl = React.useState(Router.currentUrl())
    React.router [
        router.onUrlChanged updateCurrentUrl
        router.children [
            match currentUrl with
            | [ ] -> Counter()
            | [ "styled" ] -> StyledCounter()
            | [ "server" ] -> ServerCounter()
            | [ "bulma" ] -> BulmaCounter()
            | [ "about" ] -> About "Feliz"
            | _ -> Html.h1 "Not found"
        ]
    ]