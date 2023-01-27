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
            table

        [<CustomOperation "title_text">]
        member __.TitleText(table: Table, text: string) = 
            table.Title <- TableTitle(text)
            table

        [<CustomOperation "width">]
        member __.Width(table: Table, width: int) = 
            table.Width <- width
            table

        [<CustomOperation "no_headers">]
        member __.NoHeaders(table: Table) = table.HideHeaders()

        [<CustomOperation "no_footers">]
        member __.NoFooters(table: Table) = table.HideFooters()

        [<CustomOperation "border">]
        member __.Border(table: Table, tableBorder: TableBorder) = 
            table.Border <- tableBorder
            table

        [<CustomOperation "no_border">]
        member __.NoBorder(table: Table) = table.NoBorder()

        [<CustomOperation "column">]
        member __.Column(table: Table, column: TableColumn) = table.AddColumn(column)

        [<CustomOperation "columns">]
        member __.Columns(table: Table, column: TableColumn array) = table.AddColumns(column)

        [<CustomOperation "column_text">]
        member __.ColumnText(table: Table, text: string) = table.AddColumn(text)

        [<CustomOperation "columns_text">]
        member __.ColumnsText(table: Table, texts: string array) = table.AddColumns(texts)

        [<CustomOperation "empty_row">]
        member __.EmptyRow(table: Table) = table.AddEmptyRow()

        [<CustomOperation "row">]
        member __.Row(table: Table, columns: IRenderable array) = table.AddRow(columns)

        [<CustomOperation "row_text">]
        member __.RowText(table: Table, columns: string array) = table.AddRow(columns)

    
    let table = TableBuilder()