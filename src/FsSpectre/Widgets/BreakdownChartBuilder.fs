namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module BreakdownChartBuilder =

    type BreakdownChartConfig =
        { Compact: bool
          ShowPercentage: bool
          ShowTags: bool
          ShowTagValues: bool
          Items: (string * float * Color) array }

        static member Default =
            { Compact = true
              ShowPercentage = false
              ShowTags = true
              ShowTagValues = true
              Items = Array.empty<(string * float * Color)> }

    type BreakdownChartBuilder() =
        member __.Yield _ = BreakdownChartConfig.Default

        member __.Run(config: BreakdownChartConfig) =
            let result = BreakdownChart()
            result.Compact <- config.Compact
            result.ShowTags <- config.ShowTags
            result.ShowTagValues <- config.ShowTagValues

            if config.ShowPercentage then
                result.ShowPercentage() |> ignore

            config.Items
            |> Array.iter (fun (label, value, color) -> result.AddItem(label, value, color) |> ignore)

            result

        [<CustomOperation "full_size">]
        member __.FullSize(config: BreakdownChartConfig) = { config with Compact = false }

        [<CustomOperation "show_percentage">]
        member __.ShowPercentage(config: BreakdownChartConfig) = { config with ShowPercentage = true }

        [<CustomOperation "hide_tags">]
        member __.HideTags(config: BreakdownChartConfig) = { config with ShowTags = false }

        [<CustomOperation "hide_tag_values">]
        member __.HideTagValues(config: BreakdownChartConfig) = { config with ShowTagValues = false }

        [<CustomOperation "item">]
        member __.Item(config: BreakdownChartConfig, item: (string * float * Color)) =
            { config with
                Items = Array.append config.Items [| item |] }

        [<CustomOperation "items">]
        member __.Items(config: BreakdownChartConfig, items: 'T array, converter: 'T -> (string * float * Color)) =
            { config with
                Items = items |> Array.map converter |> Array.append config.Items }

    let breakdownChart = BreakdownChartBuilder()
