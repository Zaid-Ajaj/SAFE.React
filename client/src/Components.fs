module Components

open Feliz

[<ReactComponent>]
let counter() =
    let (count, setCount) = React.useState(0)
    Html.div [
        prop.style [ style.padding 20 ]
        prop.children [
            Html.h1 count
            Html.button [
                prop.text "Increment"
                prop.onClick (fun _ -> setCount(count + 1))
            ]
        ]
    ]
