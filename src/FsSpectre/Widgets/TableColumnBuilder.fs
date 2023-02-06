namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TableColumnBuilder =

    type TableColumnConfig =
        { HeaderText: string
          HeaderRenderable: IRenderable option
          Footer: IRenderable option
          NoWrap: bool
          Width: int option
          Alignment: Justify option
          Padding: Padding }

        static member Default =
            { HeaderText = String.Empty
              HeaderRenderable = None
              Footer = None
              NoWrap = false
              Width = None
              Alignment = None
              Padding = Padding(1, 0, 1, 0) }

    type TableColumnBuilder() =
        member __.Yield _ = TableColumnConfig.Default

        member __.Run(config: TableColumnConfig) =
            let result =
                match config.HeaderRenderable with
                | Some h -> TableColumn(h)
                | None -> TableColumn(config.HeaderText)

            config.Footer |> Option.iter (fun f -> result.Footer <- f)
            result.NoWrap <- config.NoWrap
            config.Alignment |> Option.iter (fun a -> result.Alignment <- a)
            result.Padding <- config.Padding
            result

        [<CustomOperation "header_text">]
        member __.HeaderText(config: TableColumnConfig, text: string) =
            { config with
                HeaderText = text
                HeaderRenderable = None }

        [<CustomOperation "header_renderable">]
        member __.HeaderRenderable(config: TableColumnConfig, renderable: IRenderable) =
            { config with
                HeaderText = String.Empty
                HeaderRenderable = Some renderable }

        [<CustomOperation "footer_text">]
        member __.FooterText(config: TableColumnConfig, text: string) =
            let renderable = Markup(text)
            { config with Footer = Some renderable }

        [<CustomOperation "footer_renderable">]
        member __.FooterRenderable(config: TableColumnConfig, renderable: IRenderable) =
            { config with Footer = Some renderable }

        [<CustomOperation "no_wrap">]
        member __.NoWrap(config: TableColumnConfig) = { config with NoWrap = true }

        [<CustomOperation "left_aligned">]
        member __.LeftAligned(config: TableColumnConfig) =
            { config with
                Alignment = Some Justify.Left }

        [<CustomOperation "centerd">]
        member __.Centered(config: TableColumnConfig) =
            { config with
                Alignment = Some Justify.Center }

        [<CustomOperation "right_aligned">]
        member __.RightAligned(config: TableColumnConfig) =
            { config with
                Alignment = Some Justify.Right }

        [<CustomOperation "width">]
        member __.Width(config: TableColumnConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "pad_left">]
        member __.PadLeft(config: TableColumnConfig, pad: int) =
            { config with
                Padding = Padding(pad, config.Padding.Top, config.Padding.Right, config.Padding.Bottom) }

        [<CustomOperation "pad_top">]
        member __.PadTop(config: TableColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, pad, config.Padding.Right, config.Padding.Bottom) }

        [<CustomOperation "pad_right">]
        member __.PadRight(config: TableColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, config.Padding.Top, pad, config.Padding.Bottom) }

        [<CustomOperation "pad_bottom">]
        member __.PadBottom(config: TableColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, config.Padding.Top, config.Padding.Right, pad) }

    let tableColumn = TableColumnBuilder()
