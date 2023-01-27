namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module GridColumnBuilder =

    type GridColumnBuilder() =
        member __.Yield _ = GridColumn()

        [<CustomOperation "no_wrap">]
        member __.NoWrap(gridColumn: GridColumn) =
            gridColumn.NoWrap <- true
            gridColumn

        [<CustomOperation "left_aligned">]
        member __.LeftAligned(gridColumn: GridColumn) = gridColumn.LeftAligned()

        [<CustomOperation "centerd">]
        member __.Centered(gridColumn: GridColumn) = gridColumn.Centered()

        [<CustomOperation "right_aligned">]
        member __.RightAligned(gridColumn: GridColumn) = gridColumn.RightAligned()

        [<CustomOperation "width">]
        member __.Width(gridColumn: GridColumn, width: int) =
            gridColumn.Width <- width
            gridColumn

        [<CustomOperation "pad_right">]
        member __.PadRight(gridColumn: GridColumn, pad: int) = gridColumn.PadRight(pad)

        [<CustomOperation "pad_left">]
        member __.PadLeft(gridColumn: GridColumn, pad: int) = gridColumn.PadLeft(pad)

        [<CustomOperation "pad_top">]
        member __.PadTop(gridColumn: GridColumn, pad: int) = gridColumn.PadTop(pad)

        [<CustomOperation "pad_bottom">]
        member __.PadBottom(gridColumn: GridColumn, pad: int) = gridColumn.PadBottom(pad)

    let gridColumn = GridColumnBuilder()