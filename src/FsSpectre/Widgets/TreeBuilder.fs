namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TreeBuilder =

    type TreeConfig =
        { Label: IRenderable
          Nodes: TreeNode array }

        static member Default =
            { Label = Markup(String.Empty)
              Nodes = Array.empty<TreeNode> }

    type TreeBuilder() =
        member __.Yield _ = TreeConfig.Default

        member __.Run(config: TreeConfig) = 
            let result = Tree(config.Label)
            result.AddNodes(config.Nodes)
            result

        [<CustomOperation "label">]
        member __.RootText(config: TreeConfig, text: string) = { config with Label = Markup(text) }

        [<CustomOperation "label_renderable">]
        member __.LabelRenderable(config: TreeConfig, renderable: IRenderable) = { config with Label = renderable }

        [<CustomOperation "node">]
        member __.Node(config: TreeConfig, subNode: TreeNode) =
            { config with
                Nodes = Array.append config.Nodes [| subNode |] }

        [<CustomOperation "nodes">]
        member __.Nodes(config: TreeConfig, subNodes: TreeNode array) =
            { config with
                Nodes = Array.append config.Nodes subNodes }

        [<CustomOperation "node_renderable">]
        member __.NodeRenderable(config: TreeConfig, renderable: IRenderable) =
            { config with
                Nodes = Array.append config.Nodes [| TreeNode(renderable) |] }

        [<CustomOperation "node_text">]
        member __.NodeText(config: TreeConfig, text: string) =
            { config with
                Nodes = Array.append config.Nodes [| TreeNode(Markup(text)) |] }

    let tree = TreeBuilder()
