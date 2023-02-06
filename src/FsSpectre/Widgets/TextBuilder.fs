namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TextBuilder =

    type TextConfig =
        { Text: string
          Style: Style
          Justification: Justify }

        static member Default =
            { Text = String.Empty
              Style = Style.Plain
              Justification = Justify.Left }

    type TextBuilder() =
        member __.Yield _ = TextConfig.Default

        member __.Run(config: TextConfig) =
            let result = Text(config.Text, config.Style)
            result.Justify(config.Justification)

        [<CustomOperation "empty">]
        member __.Empty(config: TextConfig) = { config with Text = String.Empty }

        [<CustomOperation "text">]
        member __.Text(config: TextConfig, text: string) = { config with Text = text }

        [<CustomOperation "style">]
        member __.Style(config: TextConfig, style: Style) = { config with Style = style }

        [<CustomOperation "left_justified">]
        member __.LeftJustified(config: TextConfig) =
            { config with
                Justification = Justify.Left }

        [<CustomOperation "right_justified">]
        member __.RightJustified(config: TextConfig) =
            { config with
                Justification = Justify.Right }

        [<CustomOperation "centered">]
        member __.Centered(config: TextConfig) =
            { config with
                Justification = Justify.Center }

    let text = TextBuilder()
