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

        [<CustomOperation "node">]
        member __.Node(node: TreeNode, subNode: TreeNode) = 
            node.AddNode(subNode) |> ignore
            node

        [<CustomOperation "nodes">]
        member __.Nodes(node: TreeNode, subNodes: TreeNode array) = 
            node.AddNodes(subNodes) |> ignore
            node

        [<CustomOperation "node_renderable">]
        member __.NodeRenderable(node: TreeNode, renderable: IRenderable) = 
            node.AddNode(renderable) |> ignore
            node

        [<CustomOperation "node_text">]
        member __.NodeText(node: TreeNode, text: string) = 
            node.AddNode(text) |> ignore
            node

    let treeNode: TreeNodeBuilder = TreeNodeBuilder()
