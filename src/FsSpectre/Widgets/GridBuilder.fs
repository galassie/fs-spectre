namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module GridBuilder =

    type GridConfig =
        { Expand: bool
          ExplicitNumberOfColumns: int option
          Columns: GridColumn array
          Rows: IRenderable array array }

        static member Default =
            { Expand = false
              ExplicitNumberOfColumns = None
              Columns = Array.empty<GridColumn>
              Rows = Array.empty<IRenderable array> }

    type GridBuilder() =
        member __.Yield _ = GridConfig.Default

        member __.Run(config: GridConfig) =
            let result = Grid()
            result.Expand <- config.Expand

            if Option.isSome config.ExplicitNumberOfColumns then
                config.ExplicitNumberOfColumns
                |> Option.iter (fun n -> result.AddColumns(n) |> ignore)
            else
                result.AddColumns(config.Columns) |> ignore

            config.Rows |> Array.iter (fun row -> result.AddRow(row) |> ignore)
            result

        [<CustomOperation "expand">]
        member __.Expand(config: GridConfig) = { config with Expand = true }

        [<CustomOperation "number_of_columns">]
        member __.NodeText(config: GridConfig, count: int) =
            { config with
                ExplicitNumberOfColumns = Some count }

        [<CustomOperation "empty_column">]
        member __.Column(config: GridConfig) =
            { config with
                Columns = Array.append config.Columns [| GridColumn() |] }

        [<CustomOperation "column">]
        member __.Column(config: GridConfig, column: GridColumn) =
            { config with
                Columns = Array.append config.Columns [| column |] }

        [<CustomOperation "empty_row">]
        member __.Row(config: GridConfig) =
            { config with
                Rows = Array.append config.Rows [| [||] |] }

        [<CustomOperation "row">]
        member __.Row(config: GridConfig, columns: IRenderable array) =
            { config with
                Rows = Array.append config.Rows [| columns |] }

        [<CustomOperation "row_text">]
        member __.RowText(config: GridConfig, columns: string array) =
            let markups = columns |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Rows = Array.append config.Rows [| markups |] }

    let grid = GridBuilder()
