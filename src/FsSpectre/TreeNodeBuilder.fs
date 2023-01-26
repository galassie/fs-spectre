namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TreeNodeBuilder =

    type TreeNodeBuilder() =
        member __.Yield _ = TreeNode(Markup(String.Empty))

        [<CustomOperation "root_renderable">]
        member __.RootRenderable(_, renderable: IRenderable) = TreeNode(renderable)

        [<CustomOperation "root_text">]
        member __.RootText(_, text: string) = TreeNode(Markup(text))

        [<CustomOperation "add_node">]
        member __.AddNode(node: TreeNode, subNode: TreeNode) = 
            node.AddNode(subNode) |> ignore
            node

        [<CustomOperation "add_nodes">]
        member __.AddNodes(node: TreeNode, subNodes: TreeNode array) = 
            node.AddNodes(subNodes) |> ignore
            node

        [<CustomOperation "add_node_renderable">]
        member __.AddNodeRenderable(node: TreeNode, renderable: IRenderable) = 
            node.AddNode(renderable) |> ignore
            node

        [<CustomOperation "add_node_text">]
        member __.AddNodeText(node: TreeNode, text: string) = 
            node.AddNode(text) |> ignore
            node

    let treeNode: TreeNodeBuilder = TreeNodeBuilder()
