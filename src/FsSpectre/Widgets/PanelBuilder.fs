namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module PanelBuilder =

    type PanelBuilder() =
        member __.Yield _ = Panel(String.Empty)

        [<CustomOperation "content">]
        member __.Content(_, text: string) = Panel(text)

        [<CustomOperation "content_renderable">]
        member __.ContentRenderable(_, renderable: IRenderable) = Panel(renderable)

        [<CustomOperation "border_color">]
        member __.BorderColor(panel: Panel, color: Color) = panel.BorderColor(color)

        [<CustomOperation "header">]
        member __.Header(panel: Panel, header: PanelHeader) =
            panel.Header <- header
            panel

    let panel = PanelBuilder()
