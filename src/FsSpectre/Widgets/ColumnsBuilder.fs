namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module ColumnsBuilder =

    type ColumnsBuilder() =
        member __.Yield _ = Columns(Array.empty<IRenderable>)

        [<CustomOperation "items">]
        member __.Items(_, items: string array) = Columns(items)

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(_, items: IRenderable array) = Columns(items)

    let columns = ColumnsBuilder()
