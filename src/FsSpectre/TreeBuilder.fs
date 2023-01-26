namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module TreeBuilder =

    type TreeBuilder() =
        member __.Yield _ = Tree(Markup(String.Empty))

        [<CustomOperation "root_renderable">]
        member __.RootRenderable(_, renderable: IRenderable) = Tree(renderable)

        [<CustomOperation "root_text">]
        member __.RootText(_, text: string) = Tree(Markup(text))

        [<CustomOperation "add_node">]
        member __.AddNode(tree: Tree, subNode: TreeNode) = 
            tree.AddNode(subNode) |> ignore
            tree

        [<CustomOperation "add_nodes">]
        member __.AddNodes(tree: Tree, subNodes: TreeNode array) = 
            tree.AddNodes(subNodes) |> ignore
            tree

        [<CustomOperation "add_node_renderable">]
        member __.AddNodeRenderable(tree: Tree, renderable: IRenderable) = 
            tree.AddNode(renderable) |> ignore
            tree

        [<CustomOperation "add_node_text">]
        member __.AddNodeText(tree: Tree, text: string) = 
            tree.AddNode(text) |> ignore
            tree
    
    let tree = TreeBuilder()