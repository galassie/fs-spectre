namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module GridBuilder =

    type GridBuilder() =
        member __.Yield _ = Grid()

        [<CustomOperation "expand">]
        member __.Expand(grid: Grid) =
            grid.Expand <- true
            grid

        [<CustomOperation "number_of_columns">]
        member __.NodeText(grid: Grid, count: int) = grid.AddColumns(count)

        [<CustomOperation "column">]
        member __.Column(grid: Grid, column: GridColumn) = grid.AddColumn(column)

        [<CustomOperation "row">]
        member __.Row(grid: Grid, columns: IRenderable array) = grid.AddRow(columns)

        [<CustomOperation "row_text">]
        member __.RowText(grid: Grid, columns: string array) = grid.AddRow(columns)

    let grid = GridBuilder()
