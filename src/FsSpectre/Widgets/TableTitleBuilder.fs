namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TableTitleBuilder =

    type TableTitleConfig =
        { Text: string
          Style: Style }

        static member Default =
            { Text = String.Empty
              Style = Style.Plain }

    type TableTitleBuilder() =

        member __.Yield _ = TableTitleConfig.Default

        member __.Run(config: MarkupConfig) = TableTitle(config.Text, config.Style)

        [<CustomOperation "empty">]
        member __.Empty(config: TableTitleConfig) = { config with Text = String.Empty }

        [<CustomOperation "text">]
        member __.Text(config: TableTitleConfig, text: string) = { config with Text = text }

        [<CustomOperation "style">]
        member __.Style(config: TableTitleConfig, style: Style) = { config with Style = style }

    let tableTitle = TableTitleBuilder()
