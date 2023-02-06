namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module MarkupBuilder =

    type MarkupConfig =
        { Text: string
          Style: Style
          Justification: Justify }

        static member Default =
            { Text = String.Empty
              Style = Style.Plain
              Justification = Justify.Left }

    type MarkupBuilder() =
        member __.Yield _ = MarkupConfig.Default

        member __.Run(config: MarkupConfig) =
            let result = Markup(config.Text, config.Style)
            result.Justify(config.Justification)

        [<CustomOperation "empty">]
        member __.Empty(config: MarkupConfig) = { config with Text = String.Empty }

        [<CustomOperation "text">]
        member __.Text(config: MarkupConfig, text: string) = { config with Text = text }

        [<CustomOperation "style">]
        member __.Style(config: MarkupConfig, style: Style) = { config with Style = style }

        [<CustomOperation "left_justified">]
        member __.LeftJustified(config: MarkupConfig) =
            { config with
                Justification = Justify.Left }

        [<CustomOperation "right_justified">]
        member __.RightJustified(config: MarkupConfig) =
            { config with
                Justification = Justify.Right }

        [<CustomOperation "centered">]
        member __.Centered(config: MarkupConfig) =
            { config with
                Justification = Justify.Center }

    let markup = MarkupBuilder()
