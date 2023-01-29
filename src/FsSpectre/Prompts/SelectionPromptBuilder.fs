namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module SelectionPromptBuilder =

    type SelectionPromptBuilder<'T>() =
        member __.Yield _ = SelectionPrompt<'T>()

        [<CustomOperation "title">]
        member __.Title(selectionPrompt: SelectionPrompt<'T>, title: string) =
            selectionPrompt.Title <- title
            selectionPrompt

        [<CustomOperation "choices">]
        member __.Choices(selectionPrompt: SelectionPrompt<'T>, choices: 'T array) = selectionPrompt.AddChoices(choices)

        [<CustomOperation "page_size">]
        member __.PageSize(selectionPrompt: SelectionPrompt<'T>, pageSize: int) =
            selectionPrompt.PageSize <- pageSize
            selectionPrompt

        [<CustomOperation "moreChoicesText">]
        member __.MoreChoicesText(selectionPrompt: SelectionPrompt<'T>, moreChoicesText: string) =
            selectionPrompt.MoreChoicesText <- moreChoicesText
            selectionPrompt

        [<CustomOperation "converter">]
        member __.Converter(selectionPrompt: SelectionPrompt<'T>, converter: 'T -> string) =
            selectionPrompt.Converter <- converter
            selectionPrompt

    let selectionPrompt<'T> = SelectionPromptBuilder<'T>()
