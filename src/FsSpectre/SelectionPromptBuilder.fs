namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module SelectionPromptBuilder =

    type SelectionPromptBuilder<'T>(choices: 'T array) =
        member __.Yield _ = SelectionPrompt<'T>().AddChoices(choices)

        [<CustomOperation "title">]
        member __.Title(selectionPrompt: SelectionPrompt<'T>, title) =
            selectionPrompt.Title <- title
            selectionPrompt

        [<CustomOperation "moreChoicesText">]
        member __.MoreChoicesText(selectionPrompt: SelectionPrompt<'T>, moreChoicesText) =
            selectionPrompt.MoreChoicesText <- moreChoicesText
            selectionPrompt

        [<CustomOperation "converter">]
        member __.Converter(selectionPrompt: SelectionPrompt<'T>, converter:'T -> string) =
            selectionPrompt.Converter <- converter
            selectionPrompt
    
    let selectionPrompt choices = SelectionPromptBuilder(choices)
