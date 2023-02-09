namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module RowsBuilder =

    type RowsConfig =
        { Items: IRenderable array
          Expand: bool }

        static member Default =
            { Items = Array.empty<IRenderable>
              Expand = true }

    type RowsBuilder() =
        member __.Yield _ = RowsConfig.Default

        member __.Run(config: RowsConfig) = 
            let result = Rows(config.Items)
            result.Expand <- config.Expand
            result

        [<CustomOperation "items_text">]
        member __.ItemsText(config: RowsConfig, items: string array) =
            let markups = items |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Items = Array.append config.Items markups }

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(config: RowsConfig, items: IRenderable array) =
            { config with
                Items = Array.append config.Items items }

        [<CustomOperation "collapse">]
        member __.Collapse(config: RowsConfig) = { config with Expand = false }


    let rows = RowsBuilder()
