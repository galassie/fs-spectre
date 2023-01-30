namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TableTitleBuilder =

    type TableTitleBuilder() =

        member __.Yield _ = TableTitle(String.Empty)

        [<CustomOperation "text">]
        member __.Text(_, text: string) = TableTitle(text)

        [<CustomOperation "style">]
        member __.Style(tableTitle: TableTitle, style: Style) = tableTitle.SetStyle(style)

    let tableTitle = TableTitleBuilder()
