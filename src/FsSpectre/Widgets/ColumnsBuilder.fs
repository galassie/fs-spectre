namespace FsSpectre

open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module ColumnsBuilder =

    type ColumnsConfig =
        { Items: IRenderable array
          Expand: bool }

        static member Default =
            { Items = Array.empty<IRenderable>
              Expand = true }

    type ColumnsBuilder() =
        member __.Yield _ = ColumnsConfig.Default

        member __.Run(config: ColumnsConfig) = 
            let result = Columns(config.Items)
            result.Expand <- false
            result

        [<CustomOperation "items_text">]
        member __.ItemsText(config: ColumnsConfig, items: string array) =
            let markups = items |> Array.map Markup |> Array.map (fun x -> x :> IRenderable)

            { config with
                Items = Array.append config.Items markups }

        [<CustomOperation "items_renderable">]
        member __.ItemsRenderable(config: ColumnsConfig, items: IRenderable array) =
            { config with
                Items = Array.append config.Items items }

        [<CustomOperation "collapse">]
        member __.Collapse(config: ColumnsConfig) = { config with Expand = false }

    let columns = ColumnsBuilder()
