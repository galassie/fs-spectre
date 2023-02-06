namespace FsSpectre

open System
open System.Collections.Generic
open Spectre.Console

[<AutoOpen>]
module MultiSelectionPromptBuilder =

    type MulitSelectionPromptConfig<'T> =
        { Title: string
          Comparer: IEqualityComparer<'T>
          Choices: ('T option * 'T array) array
          PageSize: int
          Required: bool
          MoreChoicesText: string
          InstructionsText: string
          Converter: Option<'T -> string> }

        static member Default =
            { Title = String.Empty
              Comparer = EqualityComparer<'T>.Default
              Choices = Array.empty<('T option * 'T array)>
              PageSize = 10
              Required = true
              MoreChoicesText = "[grey](Move up and down to reveal more choices)[/]"
              InstructionsText = "[grey](Press <space> to select, <enter> to accept)[/]"
              Converter = None }


    type MultiSelectionPromptBuilder<'T>() =
        member __.Yield _ = MulitSelectionPromptConfig<'T>.Default

        member __.Run(config: MulitSelectionPromptConfig<'T>) =
            let result = MultiSelectionPrompt<'T>(config.Comparer)
            result.Title <- config.Title

            config.Choices
            |> Array.iter (fun (grpOpt, choices) ->
                match grpOpt with
                | Some grp -> result.AddChoiceGroup(grp, choices) |> ignore
                | None -> result.AddChoices(choices) |> ignore)

            result.PageSize <- config.PageSize
            result.Required <- config.Required
            result.MoreChoicesText <- config.MoreChoicesText
            result.InstructionsText <- config.InstructionsText
            config.Converter |> Option.iter (fun c -> result.Converter <- c)
            result

        [<CustomOperation "title">]
        member __.Title(config: MulitSelectionPromptConfig<'T>, title: string) = { config with Title = title }

        [<CustomOperation "comparer">]
        member __.Title(config: MulitSelectionPromptConfig<'T>, comparer: IEqualityComparer<'T>) =
            { config with Comparer = comparer }

        [<CustomOperation "choices">]
        member __.Choices(config: MulitSelectionPromptConfig<'T>, choices: 'T array) =
            { config with
                Choices = Array.append config.Choices [| (None, choices) |] }

        [<CustomOperation "choice_group">]
        member __.ChoiceGroup(config: MulitSelectionPromptConfig<'T>, choiceGroup: 'T, choices: 'T array) =
            { config with
                Choices = Array.append config.Choices [| (Some choiceGroup, choices) |] }

        [<CustomOperation "not_required">]
        member __.PageSize(config: MulitSelectionPromptConfig<'T>) = { config with Required = false }

        [<CustomOperation "page_size">]
        member __.PageSize(config: MulitSelectionPromptConfig<'T>, pageSize: int) = { config with PageSize = pageSize }

        [<CustomOperation "more_choices_text">]
        member __.MoreChoicesText(config: MulitSelectionPromptConfig<'T>, moreChoicesText: string) =
            { config with
                MoreChoicesText = moreChoicesText }

        [<CustomOperation "instructions_text">]
        member __.InstructionsText(config: MulitSelectionPromptConfig<'T>, instructionsText: string) =
            { config with
                InstructionsText = instructionsText }

        [<CustomOperation "converter">]
        member __.Converter(config: MulitSelectionPromptConfig<'T>, converter: 'T -> string) =
            { config with
                Converter = Some converter }


    let multiSelectionPrompt<'T> = MultiSelectionPromptBuilder<'T>()
