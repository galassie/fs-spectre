namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TreeNodeBuilder =

    type TreeNodeConfig =
        { Label: IRenderable
          Nodes: TreeNode array }

        static member Default =
            { Label = Markup(String.Empty)
              Nodes = Array.empty<TreeNode> }

    type TreeNodeBuilder() =
        member __.Yield _ = TreeNodeConfig.Default

        member __.Run(config: TreeNodeConfig) = 
            let result = TreeNode(config.Label)
            result.AddNodes(config.Nodes)
            result

        [<CustomOperation "label">]
        member __.RootText(config: TreeNodeConfig, text: string) = { config with Label = Markup(text) }

        [<CustomOperation "label_renderable">]
        member __.LabelRenderable(config: TreeNodeConfig, renderable: IRenderable) = { config with Label = renderable }

        [<CustomOperation "node">]
        member __.Node(config: TreeNodeConfig, subNode: TreeNode) =
            { config with
                Nodes = Array.append config.Nodes [| subNode |] }

        [<CustomOperation "nodes">]
        member __.Nodes(config: TreeNodeConfig, subNodes: TreeNode array) =
            { config with
                Nodes = Array.append config.Nodes subNodes }

        [<CustomOperation "node_renderable">]
        member __.NodeRenderable(config: TreeNodeConfig, renderable: IRenderable) =
            { config with
                Nodes = Array.append config.Nodes [| TreeNode(renderable) |] }

        [<CustomOperation "node_text">]
        member __.NodeText(config: TreeNodeConfig, text: string) =
            { config with
                Nodes = Array.append config.Nodes [| TreeNode(Markup(text)) |] }

    let treeNode: TreeNodeBuilder = TreeNodeBuilder()
