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

        [<CustomOperation "header_text">]
        member __.HeaderText(panel: Panel, text: string) =
            panel.Header <- PanelHeader(text)
            panel

        [<CustomOperation "Expand">]
        member __.Expand(panel: Panel) =
            panel.Expand <- true
            panel

        [<CustomOperation "width">]
        member __.Width(panel: Panel, width: int) =
            panel.Width <- width
            panel

        [<CustomOperation "height">]
        member __.Height(panel: Panel, height: int) =
            panel.Height <- height
            panel

    let panel = PanelBuilder()
