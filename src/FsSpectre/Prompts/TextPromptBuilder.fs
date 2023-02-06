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
          Converter: Option<'T -> string> }

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
              Converter = None }

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

    let textPrompt<'T> = TextPromptBuilder<'T>()
