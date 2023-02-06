namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module GridColumnBuilder =

    type GridColumnConfig =
        { NoWrap: bool
          Alignment: Justify
          Width: int option
          Padding: Padding }

        static member Default =
            { NoWrap = false
              Alignment = Justify.Left
              Width = None
              Padding = Padding(0, 0, 0, 0) }

    type GridColumnBuilder() =
        member __.Yield _ = GridColumnConfig.Default

        member __.Run(config: GridColumnConfig) =
            let result = GridColumn()
            result.NoWrap <- config.NoWrap
            result.Alignment <- config.Alignment
            config.Width |> Option.iter (fun w -> result.Width <- w)
            result.Padding <- config.Padding
            result

        [<CustomOperation "no_wrap">]
        member __.NoWrap(config: GridColumnConfig) = { config with NoWrap = true }

        [<CustomOperation "left_aligned">]
        member __.LeftAligned(config: GridColumnConfig) =
            { config with Alignment = Justify.Left }

        [<CustomOperation "centerd">]
        member __.Centered(config: GridColumnConfig) =
            { config with
                Alignment = Justify.Center }

        [<CustomOperation "right_aligned">]
        member __.RightAligned(config: GridColumnConfig) =
            { config with
                Alignment = Justify.Right }

        [<CustomOperation "width">]
        member __.Width(config: GridColumnConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "pad_left">]
        member __.PadLeft(config: GridColumnConfig, pad: int) =
            { config with
                Padding = Padding(pad, config.Padding.Top, config.Padding.Right, config.Padding.Bottom) }

        [<CustomOperation "pad_top">]
        member __.PadTop(config: GridColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, pad, config.Padding.Right, config.Padding.Bottom) }

        [<CustomOperation "pad_right">]
        member __.PadRight(config: GridColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, config.Padding.Top, pad, config.Padding.Bottom) }

        [<CustomOperation "pad_bottom">]
        member __.PadBottom(config: GridColumnConfig, pad: int) =
            { config with
                Padding = Padding(config.Padding.Left, config.Padding.Top, config.Padding.Right, pad) }

    let gridColumn = GridColumnBuilder()
