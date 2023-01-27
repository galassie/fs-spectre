namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module BreakdownChartBuilder =

    type BreakdownChartBuilder() =
        member __.Yield _ = BreakdownChart()

        [<CustomOperation "full_size">]
        member __.FullSize(breakdownChart: BreakdownChart) = breakdownChart.FullSize()

        [<CustomOperation "show_percentage">]
        member __.ShowPercentage(breakdownChart: BreakdownChart) = breakdownChart.ShowPercentage()

        [<CustomOperation "hide_tags">]
        member __.HideTags(breakdownChart: BreakdownChart) = breakdownChart.HideTags()

        [<CustomOperation "hide_tag_values">]
        member __.HideTagValues(breakdownChart: BreakdownChart) = breakdownChart.HideTagValues()

        [<CustomOperation "item">]
        member __.Item(breakdownChart: BreakdownChart, label: string, value: float, color: Color) =
            breakdownChart.AddItem(label, value, color)

        [<CustomOperation "items">]
        member __.Items(breakdownChart: BreakdownChart, elements: 'T array, converter: 'T -> IBreakdownChartItem) =
            breakdownChart.AddItems(elements, converter)

    let breakdownChart = BreakdownChartBuilder()
