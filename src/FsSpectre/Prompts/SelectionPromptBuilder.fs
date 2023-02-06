namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module SelectionPromptBuilder =

    type SelectionPromptConfig<'T> =
        { Title: string
          Choices: 'T array
          PageSize: int
          MoreChoicesText: string
          Converter: Option<'T -> string> }

        static member Default =
            { Title = String.Empty
              Choices = Array.empty<'T>
              PageSize = 10
              MoreChoicesText = "[grey](Move up and down to reveal more choices)[/]"
              Converter = None }

    type SelectionPromptBuilder<'T>() =
        member __.Yield _ = SelectionPromptConfig<'T>.Default

        member __.Run(config: SelectionPromptConfig<'T>) =
            let result = SelectionPrompt<'T>()
            result.Title <- config.Title
            result.AddChoices(config.Choices) |> ignore
            result.PageSize <- config.PageSize
            result.MoreChoicesText <- config.MoreChoicesText
            config.Converter |> Option.iter (fun c -> result.Converter <- c)
            result

        [<CustomOperation "title">]
        member __.Title(config: SelectionPromptConfig<'T>, title: string) = { config with Title = title }

        [<CustomOperation "choices">]
        member __.Choices(config: SelectionPromptConfig<'T>, choices: 'T array) =
            { config with
                Choices = Array.append config.Choices choices }

        [<CustomOperation "page_size">]
        member __.PageSize(config: SelectionPromptConfig<'T>, pageSize: int) = { config with PageSize = pageSize }

        [<CustomOperation "more_choices_text">]
        member __.MoreChoicesText(config: SelectionPromptConfig<'T>, moreChoicesText: string) =
            { config with
                MoreChoicesText = moreChoicesText }

        [<CustomOperation "converter">]
        member __.Converter(config: SelectionPromptConfig<'T>, converter: 'T -> string) =
            { config with
                Converter = Some converter }

    let selectionPrompt<'T> = SelectionPromptBuilder<'T>()
