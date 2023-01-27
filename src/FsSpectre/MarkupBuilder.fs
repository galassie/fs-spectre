namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module MarkupBuilder =

    type MarkupBuilder() =
        member __.Yield _ = Markup(String.Empty)

        [<CustomOperation "text">]
        member __.Text(_, text: string) = Markup(text)

        [<CustomOperation "text_with_style">]
        member __.TextWithStyle(_, text: string, style: Style) = Markup(text, style)

        [<CustomOperation "left_justified">]
        member __.LeftJustified(markup: Markup) = markup.LeftJustified()

        [<CustomOperation "right_justified">]
        member __.RightJustified(markup: Markup) = markup.RightJustified()

        [<CustomOperation "centered">]
        member __.Centered(markup: Markup) = markup.Centered()

    let markup = MarkupBuilder()
