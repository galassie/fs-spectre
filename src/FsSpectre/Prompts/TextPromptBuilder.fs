namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TextPromptBuilder =

    type TextPromptConfig<'T> =
        { Text: string
          Comparer: StringComparer option
          PromptStyle: Style
          IsSecret: bool
          Mask: char option
          HasHiddenInput: bool
          AllowEmpty: bool
          ValidationErrorMessage: string
          Validator: Option<'T -> ValidationResult>
          Converter: Option<'T -> string>
          ShowChoices: bool
          ChoicesStyle: Style option
          Choices: 'T array
          ShowDefaultValue: bool
          DefaultValueStyle: Style option
          DefaultValue: 'T option }

        static member Default =
            { Text = String.Empty
              Comparer = None
              PromptStyle = Style.Plain
              IsSecret = false
              Mask = None
              HasHiddenInput = false
              AllowEmpty = false
              ValidationErrorMessage = "[red]Invalid input[/]"
              Validator = None
              Converter = None
              ShowChoices = false
              ChoicesStyle = None
              Choices = Array.empty<'T>
              ShowDefaultValue = true
              DefaultValueStyle = None
              DefaultValue = None }

    type TextPromptBuilder<'T>() =
        member __.Yield _ = TextPromptConfig<'T>.Default

        member __.Run(config: TextPromptConfig<'T>) =
            let result = TextPrompt<'T>(config.Text, Option.toObj config.Comparer)
            result.IsSecret <- config.IsSecret

            if config.HasHiddenInput then
                result.Mask <- Nullable()
            else
                config.Mask |> Option.iter (fun m -> result.Mask <- m)

            result.AllowEmpty <- config.AllowEmpty
            result.ValidationErrorMessage <- config.ValidationErrorMessage
            config.Validator |> Option.iter (fun v -> result.Validator <- v)
            config.Converter |> Option.iter (fun c -> result.Converter <- c)

            result.ShowChoices <- config.ShowChoices
            result.AddChoices(config.Choices) |> ignore
            config.ChoicesStyle |> Option.iter (fun cs -> result.ChoicesStyle <- cs)

            result.ShowDefaultValue <- config.ShowDefaultValue
            config.DefaultValueStyle |> Option.iter (fun vs -> result.DefaultValueStyle <- vs)
            config.DefaultValue |> Option.iter (fun dv -> result.DefaultValue(dv) |> ignore)

            result

        [<CustomOperation "text">]
        member __.Text(config: TextPromptConfig<'T>, text: string) = { config with Text = text }

        [<CustomOperation "comparer">]
        member __.Comparer(config: TextPromptConfig<'T>, comparer: StringComparer) =
            { config with Comparer = Some comparer }

        [<CustomOperation "prompt_style">]
        member __.PromptStyle(config: TextPromptConfig<'T>, style: Style) = { config with PromptStyle = style }

        [<CustomOperation "secret">]
        member __.Secret(config: TextPromptConfig<'T>) = { config with IsSecret = true }

        [<CustomOperation "secret_with_mask">]
        member __.SecretWithMask(config: TextPromptConfig<'T>, mask: char) =
            { config with
                IsSecret = true
                Mask = Some mask
                HasHiddenInput = false }

        [<CustomOperation "secret_with_hidden_input">]
        member __.SecretWithHiddenInput(config: TextPromptConfig<'T>) =
            { config with
                IsSecret = true
                Mask = None
                HasHiddenInput = true }

        [<CustomOperation "allow_empty">]
        member __.AllowEmpty(config: TextPromptConfig<'T>) = { config with AllowEmpty = true }

        [<CustomOperation "validation_error_message">]
        member __.ValidationErrorMessage(config: TextPromptConfig<'T>, validationErrorMessage: string) =
            { config with
                ValidationErrorMessage = validationErrorMessage }

        [<CustomOperation "validator">]
        member __.Validator(config: TextPromptConfig<'T>, validator: 'T -> ValidationResult) =
            { config with
                Validator = Some validator }

        [<CustomOperation "converter">]
        member __.Converter(config: TextPromptConfig<'T>, converter: 'T -> string) =
            { config with
                Converter = Some converter }

        [<CustomOperation "show_choices">]
        member __.ShowChoices(config: TextPromptConfig<'T>) =
            { config with
                ShowChoices = true }

        [<CustomOperation "choices_style">]
        member __.ChoicesStyle(config: TextPromptConfig<'T>, style: Style) =
            { config with
                ChoicesStyle = Some style }

        [<CustomOperation "choices">]
        member __.Choices(config: TextPromptConfig<'T>, choices: 'T array) =
            { config with
                Choices = Array.append config.Choices choices }

        [<CustomOperation "hide_default_value">]
        member __.HideDefaultValue(config: TextPromptConfig<'T>) =
            { config with
                ShowDefaultValue = false }

        [<CustomOperation "default_value_style">]
        member __.DefaultValueStyle(config: TextPromptConfig<'T>, style: Style) =
            { config with
                ChoicesStyle = Some style }

        [<CustomOperation "default_value">]
        member __.DefaultValue(config: TextPromptConfig<'T>, value: 'T) =
            { config with
                DefaultValue = Some value }

    let textPrompt<'T> = TextPromptBuilder<'T>()
