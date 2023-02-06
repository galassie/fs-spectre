namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module RuleBuilder =

    type RuleConfig =
        { Title: string
          Justification: Justify }

        static member Default =
            { Title = String.Empty
              Justification = Justify.Left }

    type RuleBuilder() =
        member __.Yield _ = RuleConfig.Default

        member __.Run(config: RuleConfig) =
            let result = Rule(config.Title)
            result.Justify(config.Justification)

        [<CustomOperation "empty">]
        member __.Empty(config: RuleConfig) = { config with Title = String.Empty }

        [<CustomOperation "title">]
        member __.Title(config: RuleConfig, title: string) = { config with Title = title }

        [<CustomOperation "left_justified">]
        member __.LeftJustified(config: RuleConfig) =
            { config with
                Justification = Justify.Left }

        [<CustomOperation "right_justified">]
        member __.RightJustified(config: RuleConfig) =
            { config with
                Justification = Justify.Right }

        [<CustomOperation "centered">]
        member __.Centered(config: RuleConfig) =
            { config with
                Justification = Justify.Center }

    let rule = RuleBuilder()
