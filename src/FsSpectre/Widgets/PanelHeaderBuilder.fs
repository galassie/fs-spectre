namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module PanelHeaderBuilder =

    type PanelHeaderConfig =
        { Text: string
          Justification: Justify }

        static member Default =
            { Text = String.Empty
              Justification = Justify.Left }

    type PanelHeaderBuilder() =
        member __.Yield _ = PanelHeaderConfig.Default

        member __.Run(config: PanelHeaderConfig) =
            let result = PanelHeader(config.Text)
            result.Justify(config.Justification)

        [<CustomOperation "empty">]
        member __.Empty(config: PanelHeaderConfig) = { config with Text = String.Empty }

        [<CustomOperation "text">]
        member __.Text(config: PanelHeaderConfig, text: string) = { config with Text = text }

        [<CustomOperation "left_justified">]
        member __.LeftJustified(config: PanelHeaderConfig) =
            { config with
                Justification = Justify.Left }

        [<CustomOperation "right_justified">]
        member __.RightJustified(config: PanelHeaderConfig) =
            { config with
                Justification = Justify.Right }

        [<CustomOperation "centered">]
        member __.Centered(config: PanelHeaderConfig) =
            { config with
                Justification = Justify.Center }

    let panelHeader = PanelHeaderBuilder()
