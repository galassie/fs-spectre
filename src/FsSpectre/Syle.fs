namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module StyleBuilder =

    type StyleConfig =
        { Foreground: Color
          Background: Color
          Decoration: Decoration
          Link: string option }

        static member Default =
            { Foreground = Color.Default
              Background = Color.Default
              Decoration = Decoration.None
              Link = None }

    type StyleBuilder() =
        member __.Yield _ = StyleConfig.Default

        member __.Run(config: StyleConfig) =
            Style(config.Foreground, config.Background, config.Decoration, Option.toObj config.Link)

        [<CustomOperation "plain">]
        member __.Plain(config: StyleConfig) =
            let plain = Style.Plain

            { config with
                Foreground = plain.Foreground
                Background = plain.Background
                Decoration = plain.Decoration
                Link = None }

        [<CustomOperation "foreground">]
        member __.Foreground(config: StyleConfig, foreground: Color) = { config with Foreground = foreground }

        [<CustomOperation "background">]
        member __.Background(config: StyleConfig, background: Color) = { config with Background = background }

        [<CustomOperation "decoration">]
        member __.Decoration(config: StyleConfig, decoration: Decoration) = { config with Decoration = decoration }

        [<CustomOperation "link">]
        member __.Link(config: StyleConfig, link: string) = { config with Link = Some link }

    let style = StyleBuilder()
