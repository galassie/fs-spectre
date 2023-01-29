namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module TextPromptBuilder =

    type TextPromptBuilder<'T>() =
        member __.Yield _ = TextPrompt<'T>(String.Empty)

        [<CustomOperation "text">]
        member __.Title(_, text: string) = TextPrompt<'T>(text)

        [<CustomOperation "prompt_style">]
        member __.PromptStyle(textPrompt: TextPrompt<'T>, style: Style) = 
            textPrompt.PromptStyle <- style
            textPrompt

        [<CustomOperation "secret">]
        member __.Secret(textPrompt: TextPrompt<'T>) = textPrompt.Secret()

        [<CustomOperation "mask">]
        member __.Mask(textPrompt: TextPrompt<'T>, mask: char) = textPrompt.Secret(mask)

        [<CustomOperation "hide_input">]
        member __.HideInput(textPrompt: TextPrompt<'T>) = textPrompt.Secret(Nullable())

        [<CustomOperation "allow_empty">]
        member __.AllowEmpty(textPrompt: TextPrompt<'T>) = 
            textPrompt.AllowEmpty <- true
            textPrompt
        
        [<CustomOperation "validation_error_message">]
        member __.ValidationErrorMessage(textPrompt: TextPrompt<'T>, validationErrorMessage: string) = 
            textPrompt.ValidationErrorMessage <- validationErrorMessage
            textPrompt
        
        [<CustomOperation "validate">]
        member __.Validate(textPrompt: TextPrompt<'T>, validator: 'T -> ValidationResult) = 
            textPrompt.Validator <- validator
            textPrompt

    let textPrompt<'T> = TextPromptBuilder<'T>()