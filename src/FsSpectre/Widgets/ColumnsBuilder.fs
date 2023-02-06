namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module ColumnsBuilder =

    type ColumnsConfig =
        { Items: IRenderable array }

        static member Default = { Items = Array.empty<IRenderable> }

    type ColumnsBuilder() =
        member __.Yield _ = ColumnsConfig.Default

        member __.Run(config: ColumnsConfig) = Columns(config.Items)

        [<CustomOperation "items_text">]
        member __.ItemsText(config: ColumnsConfig, items: string array) =
            let markups = items |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Items = Array.append config.Items markups }

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(config: ColumnsConfig, items: IRenderable array) =
            { config with
                Items = Array.append config.Items items }

    let columns = ColumnsBuilder()
