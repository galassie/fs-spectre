namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TextBuilder =

    type TextBuilder() =
        member __.Yield _ = Text(String.Empty)

        [<CustomOperation "empty">]
        member __.Text(text: Text) = text

        [<CustomOperation "text">]
        member __.Text(_, text: string) = Text(text)

        [<CustomOperation "text_with_style">]
        member __.TextWithStyle(_, text: string, style: Style) = Text(text, style)

        [<CustomOperation "left_justified">]
        member __.LeftJustified(text: Text) = text.LeftJustified()

        [<CustomOperation "right_justified">]
        member __.RightJustified(text: Text) = text.RightJustified()

        [<CustomOperation "centered">]
        member __.Centered(text: Text) = text.Centered()

    let text = TextBuilder()
