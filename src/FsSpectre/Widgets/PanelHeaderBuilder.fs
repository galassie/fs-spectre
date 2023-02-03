namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module PanelHeaderBuilder =

    type PanelHeaderBuilder() =
        member __.Yield _ = PanelHeader(String.Empty)

        [<CustomOperation "text">]
        member __.Text(_, text: string) = PanelHeader(text)

        [<CustomOperation "left_justified">]
        member __.LeftJustified(panelHeader: PanelHeader) = panelHeader.LeftJustified()

        [<CustomOperation "right_justified">]
        member __.RightJustified(panelHeader: PanelHeader) = panelHeader.RightJustified()

        [<CustomOperation "centered">]
        member __.Centered(panelHeader: PanelHeader) = panelHeader.Centered()

    let panelHeader = PanelHeaderBuilder()
