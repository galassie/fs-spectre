namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module RowsBuilder =

    type RowsBuilder() =
        member __.Yield _ = Rows(Array.empty<IRenderable>)

        [<CustomOperation "items">]
        member this.Items(rows: Rows, items: string array) =
            let renderables =
                items |> Array.map (Markup) |> Array.map (fun x -> x :> IRenderable)

            this.ItemsRenderable(rows, renderables)

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(_, items: IRenderable array) = Rows(items)

    let rows = RowsBuilder()
