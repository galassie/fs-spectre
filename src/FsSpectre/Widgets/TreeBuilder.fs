namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TreeBuilder =

    type TreeBuilder() =
        member __.Yield _ = Tree(String.Empty)

        [<CustomOperation "label">]
        member __.Label(_, label: string) = Tree(label)

        [<CustomOperation "label_renderable">]
        member __.LabelRenderable(_, renderable: IRenderable) = Tree(renderable)

        [<CustomOperation "node">]
        member __.Node(tree: Tree, subNode: TreeNode) = 
            tree.AddNode(subNode) |> ignore
            tree

        [<CustomOperation "nodes">]
        member __.Nodes(tree: Tree, subNodes: TreeNode array) = 
            tree.AddNodes(subNodes) |> ignore
            tree

        [<CustomOperation "node_renderable">]
        member __.NodeRenderable(tree: Tree, renderable: IRenderable) = 
            tree.AddNode(renderable) |> ignore
            tree

        [<CustomOperation "node_text">]
        member __.NodeText(tree: Tree, text: string) = 
            tree.AddNode(text) |> ignore
            tree
    
    let tree = TreeBuilder()