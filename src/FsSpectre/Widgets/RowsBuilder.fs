namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module RowsBuilder =

    type RowsConfig =
        { Items: IRenderable array }

        static member Default = { Items = Array.empty<IRenderable> }

    type RowsBuilder() =
        member __.Yield _ = RowsConfig.Default

        member __.Run(config: RowsConfig) = Rows(config.Items)

        [<CustomOperation "items_text">]
        member __.ItemsText(config: RowsConfig, items: string array) =
            let markups = items |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Items = Array.append config.Items markups }

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(config: RowsConfig, items: IRenderable array) =
            { config with
                Items = Array.append config.Items items }


    let rows = RowsBuilder()
