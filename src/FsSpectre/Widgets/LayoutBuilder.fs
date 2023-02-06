namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module LayoutBuilder =

    type LayoutConfig =
        { Name: string
          SplitColumns: Layout array
          SplitRows: Layout array
          Ratio: int
          Width: int option
          MinimumWidth: int option
          Content: IRenderable option }

        static member Default =
            { Name = String.Empty
              SplitColumns = Array.empty<Layout>
              SplitRows = Array.empty<Layout>
              Ratio = 1
              Width = None
              MinimumWidth = None
              Content = None }

    type LayoutBuilder() =
        member __.Yield _ = LayoutConfig.Default

        member __.Run(config: LayoutConfig) =
            let result = Layout(config.Name, Option.toObj config.Content)
            config.Width |> Option.iter (fun w -> result.Size <- (Nullable w))
            config.MinimumWidth |> Option.iter (fun w -> result.MinimumSize <- w)
            result.SplitColumns(config.SplitColumns).SplitRows(config.SplitRows)

        [<CustomOperation "split_columns">]
        member __.SplitColumns(config: LayoutConfig, columns: Layout array) = { config with SplitColumns = columns }

        [<CustomOperation "split_rows">]
        member __.SplitRows(config: LayoutConfig, rows: Layout array) = { config with SplitRows = rows }

        [<CustomOperation "ratio">]
        member __.Ratio(config: LayoutConfig, ratio: int) = { config with Ratio = ratio }

        [<CustomOperation "width">]
        member __.Width(config: LayoutConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "minimum_width">]
        member __.MinimumWidth(config: LayoutConfig, width: int) =
            { config with
                MinimumWidth = Some width }

        [<CustomOperation "content">]
        member __.Content(config: LayoutConfig, content: IRenderable) = { config with Content = Some content }

    let layout = LayoutBuilder()
