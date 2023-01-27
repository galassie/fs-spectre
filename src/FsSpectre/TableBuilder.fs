namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TableBuilder =

    type TableBuilder() =

        member __.Yield _ = Table()

        [<CustomOperation "title">]
        member __.Title(table: Table, title: TableTitle) = 
            table.Title <- title

        [<CustomOperation "width">]
        member __.Width(table: Table, width: int) = 
            table.Width <- width
            table

        [<CustomOperation "hide_headers">]
        member __.HideHeaders(table: Table) = table.HideHeaders()

        [<CustomOperation "hide_footers">]
        member __.HideFooters(table: Table) = table.HideFooters()

        [<CustomOperation "border">]
        member __.Border(table: Table, tableBorder: TableBorder) = 
            table.Border <- tableBorder
            table

        [<CustomOperation "no_border">]
        member __.NoBorder(table: Table) = table.NoBorder()

        [<CustomOperation "add_column">]
        member __.AddColumn(table: Table, column: TableColumn) = table.AddColumn(column)

        [<CustomOperation "add_columns">]
        member __.AddColumns(table: Table, column: TableColumn array) = table.AddColumns(column)

        [<CustomOperation "add_column_text">]
        member __.AddColumnText(table: Table, text: string) = table.AddColumn(text)

        [<CustomOperation "add_columns_text">]
        member __.AddColumnsText(table: Table, texts: string array) = table.AddColumns(texts)

        [<CustomOperation "add_empty_row">]
        member __.AddEmptyRow(table: Table) = table.AddEmptyRow()

        [<CustomOperation "add_row">]
        member __.AddRow(table: Table, columns: IRenderable array) = table.AddRow(columns)

        [<CustomOperation "add_row_text">]
        member __.AddRow(table: Table, columns: string array) = table.AddRow(columns)

    
    let table = TableBuilder()