namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TableBuilder =

    type TableConfig =
        { Title: TableTitle option
          Columns: TableColumn array
          Rows: IRenderable array array
          ShowHeaders: bool
          ShowFooters: bool
          Width: int option
          Expand: bool
          Border: TableBorder
          BorderColor: Color option }

        static member Default =
            { Title = None
              Columns = Array.empty<TableColumn>
              Rows = Array.empty<IRenderable array>
              ShowHeaders = true
              ShowFooters = true
              Width = None
              Expand = true
              Border = TableBorder.Square
              BorderColor = None }

    type TableBuilder() =
        member __.Yield _ = TableConfig.Default

        member __.Run(config: TableConfig) =
            let result = Table()
            config.Title |> Option.iter (fun t -> result.Title <- t)
            result.AddColumns(config.Columns) |> ignore
            config.Rows |> Array.iter (fun row -> result.AddRow(row) |> ignore)
            result.ShowHeaders <- config.ShowHeaders
            result.ShowFooters <- config.ShowFooters
            config.Width |> Option.iter (fun w -> result.Width <- w)
            result.Border <- config.Border
            config.BorderColor |> Option.iter (fun c -> result.BorderColor(c) |> ignore)
            result

        [<CustomOperation "title">]
        member __.Title(config: TableConfig, title: TableTitle) = { config with Title = Some title }

        [<CustomOperation "title_text">]
        member __.TitleText(config: TableConfig, text: string) =
            let title = TableTitle(text)
            { config with Title = Some title }

        [<CustomOperation "no_headers">]
        member __.NoHeaders(config: TableConfig) = { config with ShowHeaders = false }

        [<CustomOperation "no_footers">]
        member __.NoFooters(config: TableConfig) = { config with ShowFooters = false }

        [<CustomOperation "width">]
        member __.Width(config: TableConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "collapse">]
        member __.Collapse(config: TableConfig) = { config with Expand = false }

        [<CustomOperation "simple_border">]
        member __.SimpleBorder(config: TableConfig) =
            { config with
                Border = TableBorder.Simple }

        [<CustomOperation "rounded_border">]
        member __.RoundedBorder(config: TableConfig) =
            { config with
                Border = TableBorder.Rounded }

        [<CustomOperation "ascii_border">]
        member __.AsciiBorder(config: TableConfig) =
            { config with
                Border = TableBorder.Ascii }

        [<CustomOperation "double_edge_border">]
        member __.DoubleEdgeBorder(config: TableConfig) =
            { config with
                Border = TableBorder.DoubleEdge }

        [<CustomOperation "no_border">]
        member __.NoBorder(config: TableConfig) =
            { config with
                Border = TableBorder.None }

        [<CustomOperation "border_color">]
        member __.BorderColor(config: TableConfig, color: Color) =
            { config with BorderColor = Some color }

        [<CustomOperation "column">]
        member __.Column(config: TableConfig, column: TableColumn) =
            { config with
                Columns = Array.append config.Columns [| column |] }

        [<CustomOperation "columns">]
        member __.Columns(config: TableConfig, columns: TableColumn array) =
            { config with
                Columns = Array.append config.Columns columns }

        [<CustomOperation "column_text">]
        member __.ColumnText(config: TableConfig, text: string) =
            { config with
                Columns = Array.append config.Columns [| TableColumn(text) |] }

        [<CustomOperation "columns_text">]
        member __.ColumnsText(config: TableConfig, texts: string array) =
            let columns = texts |> Array.map (fun t -> TableColumn(t))

            { config with
                Columns = Array.append config.Columns columns }

        [<CustomOperation "empty_row">]
        member __.EmptyRow(config: TableConfig) =
            { config with
                Rows = Array.append config.Rows [| [||] |] }

        [<CustomOperation "row">]
        member __.Row(config: TableConfig, columns: IRenderable array) =
            { config with
                Rows = Array.append config.Rows [| columns |] }

        [<CustomOperation "row_text">]
        member __.RowText(config: TableConfig, columns: string array) =
            let markups = columns |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Rows = Array.append config.Rows [| markups |] }


    let table = TableBuilder()
