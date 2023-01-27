namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module PanelHeaderBuilder =

    type PanelHeaderBuilder() =
        member __.Yield _ = PanelHeader(String.Empty)

        [<CustomOperation "text">]
        member __.Text(_, text: string) = PanelHeader(text)

        [<CustomOperation "justification">]
        member __.Justification(panelHeader: PanelHeader, justify: Justify) = 
            panelHeader.Justification <- justify
            panelHeader

    let panelHeader = PanelHeaderBuilder()