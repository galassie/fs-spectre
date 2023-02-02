namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module StyleBuilder =

    type StyleBuilder() =
        member __.Yield _ = Style()

        [<CustomOperation "foreground">]
        member __.Foreground(style: Style, foreground: Color) =
            Style(foreground, style.Background, style.Decoration, style.Link)

        [<CustomOperation "background">]
        member __.Background(style: Style, background: Color) =
            Style(style.Foreground, background, style.Decoration, style.Link)

        [<CustomOperation "decoration">]
        member __.Decoration(style: Style, decoration: Decoration) =
            Style(style.Foreground, style.Background, decoration, style.Link)

        [<CustomOperation "link">]
        member __.Link(style: Style, link: string) =
            Style(style.Foreground, style.Background, style.Decoration, link)

    let style = StyleBuilder()
