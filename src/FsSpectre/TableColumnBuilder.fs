namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TableColumnBuilder =

    type TableColumnBuilder() =

        member __.Yield _ = TableColumn(String.Empty)

        [<CustomOperation "header">]
        member __.Header(_, header: string) = TableColumn(header)

        [<CustomOperation "header_renderable">]
        member __.HeaderRenderable(_, renderable: IRenderable) = TableColumn(renderable)

        [<CustomOperation "footer">]
        member __.Footer(tableColumn: TableColumn, footer: string) = 
            tableColumn.Footer <- Markup(footer)
            tableColumn

        [<CustomOperation "footer_renderable">]
        member __.FooterRenderable(tableColumn: TableColumn, renderable: IRenderable) = 
            tableColumn.Footer <- renderable
            tableColumn

        [<CustomOperation "no_wrap">]
        member __.NoWrap(tableColumn: TableColumn) =
            tableColumn.NoWrap <- true
            tableColumn

        [<CustomOperation "left_aligned">]
        member __.LeftAligned(tableColumn: TableColumn) = tableColumn.LeftAligned()

        [<CustomOperation "centerd">]
        member __.Centered(tableColumn: TableColumn) = tableColumn.Centered()

        [<CustomOperation "right_aligned">]
        member __.RightAligned(tableColumn: TableColumn) = tableColumn.RightAligned()

        [<CustomOperation "width">]
        member __.Width(tableColumn: TableColumn, width: int) =
            tableColumn.Width <- width
            tableColumn

        [<CustomOperation "pad_right">]
        member __.PadRight(tableColumn: TableColumn, pad: int) = tableColumn.PadRight(pad)

        [<CustomOperation "pad_left">]
        member __.PadLeft(tableColumn: TableColumn, pad: int) = tableColumn.PadLeft(pad)

        [<CustomOperation "pad_top">]
        member __.PadTop(tableColumn: TableColumn, pad: int) = tableColumn.PadTop(pad)

        [<CustomOperation "pad_bottom">]
        member __.PadBottom(tableColumn: TableColumn, pad: int) = tableColumn.PadBottom(pad)

    let tableColumn = TableColumnBuilder()
