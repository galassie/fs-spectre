namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module BarChartBuilder =

    type BarChartConfig =
        { Label: string
          LabelAlignment: Justify
          Width: int option
          ShowValues: bool
          Items: (string * float * Color) array }

        static member Default =
            { Label = String.Empty
              LabelAlignment = Justify.Center
              Width = None
              ShowValues = true
              Items = Array.empty<(string * float * Color)> }

    type BarChartBuilder() =
        member __.Yield _ = BarChartConfig.Default

        member __.Run(config: BarChartConfig) =
            let result = BarChart()
            result.Label <- config.Label
            result.LabelAlignment <- config.LabelAlignment
            config.Width |> Option.iter (fun w -> result.Width <- w)
            result.ShowValues <- config.ShowValues

            config.Items
            |> Array.iter (fun (label, value, color) -> result.AddItem(label, value, color) |> ignore)

            result

        [<CustomOperation "label">]
        member __.FullSize(config: BarChartConfig, label: string) = { config with Label = label }

        [<CustomOperation "left_aligned_label">]
        member __.LeftAlignedLabel(config: BarChartConfig) =
            { config with
                LabelAlignment = Justify.Left }

        [<CustomOperation "centered_label">]
        member __.CenteredLabel(config: BarChartConfig) =
            { config with
                LabelAlignment = Justify.Center }

        [<CustomOperation "right_aligned_label">]
        member __.RightAlignedLabel(config: BarChartConfig) =
            { config with
                LabelAlignment = Justify.Right }

        [<CustomOperation "width">]
        member __.Width(config: BarChartConfig, width: int) = { config with Width = Some width }

        [<CustomOperation "hide_values">]
        member __.HideValues(config: BarChartConfig) = { config with ShowValues = false }

        [<CustomOperation "item">]
        member __.Item(config: BarChartConfig, item: (string * float * Color)) =
            { config with
                Items = Array.append config.Items [| item |] }

        [<CustomOperation "items">]
        member __.Items(config: BarChartConfig, items: 'T array, converter: 'T -> (string * float * Color)) =
            { config with
                Items = items |> Array.map converter |> Array.append config.Items }

    let barChart = BarChartBuilder()
