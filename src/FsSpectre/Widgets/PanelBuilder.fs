namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module PanelBuilder =

    type PanelConfig =
        { Content: IRenderable
          BorderStyle: Style option
          BorderColor: Color option
          Header: PanelHeader
          Expand: bool
          Width: int option
          Height: int option }

        static member Default =
            { Content = Markup(String.Empty)
              BorderStyle = None
              BorderColor = None
              Header = PanelHeader(String.Empty)
              Expand = false
              Width = None
              Height = None }

    type PanelBuilder() =
        member __.Yield _ = PanelConfig.Default

        member __.Run(config: PanelConfig) =
            let result = Panel(config.Content)
            config.BorderStyle |> Option.iter (fun s -> result.BorderStyle <- s)
            config.BorderColor |> Option.iter (fun c -> result.BorderColor(c) |> ignore)
            result.Header <- config.Header
            config.Width |> Option.iter (fun w -> result.Width <- (Nullable w))
            config.Height |> Option.iter (fun h -> result.Height <- (Nullable h))
            result

        [<CustomOperation "content_text">]
        member __.ContentText(config: PanelConfig, text: string) = { config with Content = Markup(text) }

        [<CustomOperation "content_renderable">]
        member __.ContentRenderable(config: PanelConfig, renderable: IRenderable) = { config with Content = renderable }

        [<CustomOperation "border_style">]
        member __.BorderStyle(config: PanelConfig, style: Style) =
            { config with BorderStyle = Some style }

        [<CustomOperation "border_color">]
        member __.BorderColor(config: PanelConfig, color: Color) =
            { config with BorderColor = Some color }

        [<CustomOperation "header">]
        member __.Header(config: PanelConfig, header: PanelHeader) = { config with Header = header }

        [<CustomOperation "header_text">]
        member __.HeaderText(config: PanelConfig, text: string) =
            { config with
                Header = PanelHeader(text) }

        [<CustomOperation "expand">]
        member __.Expand(config: PanelConfig) = { config with Expand = true }

        [<CustomOperation "width">]
        member __.Width(config: PanelConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "height">]
        member __.Height(config: PanelConfig, height: int) = { config with Height = Some height }

    let panel = PanelBuilder()
