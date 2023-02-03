namespace FsSpectre

open System
open System.Globalization
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module LayoutBuilder =

    type LayoutBuilder() =
        member __.Yield _ = Layout(String.Empty)

        [<CustomOperation "split_columns">]
        member __.SplitColumns(layout: Layout, columns: Layout array) = layout.SplitColumns(columns)

        [<CustomOperation "split_rows">]
        member __.SplitRows(layout: Layout, rows: Layout array) = layout.SplitRows(rows)

        [<CustomOperation "ratio">]
        member __.Ratio(layout: Layout, ratio: int) = 
            layout.Ratio <- ratio
            layout

        [<CustomOperation "width">]
        member __.Width(layout: Layout, width: int) = 
            layout.Size <- width
            layout

        [<CustomOperation "minimum_width">]
        member __.MinimumWidth(layout: Layout, width: int) = 
            layout.MinimumSize <- width
            layout

        [<CustomOperation "content">]
        member __.Content(layout: Layout, content: IRenderable) = 
            layout.Update(content)
    
    let layout = LayoutBuilder()
