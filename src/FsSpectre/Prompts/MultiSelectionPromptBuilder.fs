namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module MultiSelectionPromptBuilder =

    type MultiSelectionPromptBuilder<'T>() =
        member __.Yield _ = MultiSelectionPrompt<'T>()

        [<CustomOperation "title">]
        member __.Title(multiSelectionPrompt: MultiSelectionPrompt<'T>, title: string) =
            multiSelectionPrompt.Title <- title
            multiSelectionPrompt

        [<CustomOperation "choices">]
        member __.Choices(multiSelectionPrompt: MultiSelectionPrompt<'T>, choices: 'T array) =
            multiSelectionPrompt.AddChoices(choices)

        [<CustomOperation "choice_group">]
        member __.ChoiceGroup(multiSelectionPrompt: MultiSelectionPrompt<'T>, choiceGroup: 'T, choices: 'T array) =
            multiSelectionPrompt.AddChoiceGroup(choiceGroup, choices)

        [<CustomOperation "not_required">]
        member __.PageSize(multiSelectionPrompt: MultiSelectionPrompt<'T>) = multiSelectionPrompt.NotRequired()

        [<CustomOperation "page_size">]
        member __.PageSize(multiSelectionPrompt: MultiSelectionPrompt<'T>, pageSize: int) =
            multiSelectionPrompt.PageSize <- pageSize
            multiSelectionPrompt

        [<CustomOperation "more_choices_text">]
        member __.MoreChoicesText(multiSelectionPrompt: MultiSelectionPrompt<'T>, moreChoicesText: string) =
            multiSelectionPrompt.MoreChoicesText <- moreChoicesText
            multiSelectionPrompt

        [<CustomOperation "instructions_text">]
        member __.InstructionsText(multiSelectionPrompt: MultiSelectionPrompt<'T>, instructionsText: string) =
            multiSelectionPrompt.InstructionsText <- instructionsText
            multiSelectionPrompt

        [<CustomOperation "converter">]
        member __.Converter(multiSelectionPrompt: MultiSelectionPrompt<'T>, converter: 'T -> string) =
            multiSelectionPrompt.Converter <- converter
            multiSelectionPrompt

    let multiSelectionPrompt<'T> = MultiSelectionPromptBuilder<'T>()
