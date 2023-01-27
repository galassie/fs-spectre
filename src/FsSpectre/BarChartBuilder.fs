namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module BarChartBuilder =

    type BarChartBuilder() =
        member __.Yield _ = BarChart()

        [<CustomOperation "label">]
        member __.FullSize(barChart: BarChart, label: string) = 
            barChart.Label <- label
            barChart

        [<CustomOperation "left_align_label">]
        member __.LeftAlignLabel(barChart: BarChart) = barChart.LeftAlignLabel()

        [<CustomOperation "center_label">]
        member __.CenterLabel(barChart: BarChart) = barChart.CenterLabel()

        [<CustomOperation "right_align_label">]
        member __.RightAlignLabel(barChart: BarChart) = barChart.RightAlignLabel()

        [<CustomOperation "width">]
        member __.Width(barChart: BarChart, width: int) =
            barChart.Width <- width
            barChart

        [<CustomOperation "hide_values">]
        member __.HideValues(barChart: BarChart) = barChart.HideValues()

        [<CustomOperation "item">]
        member __.Item(barChart: BarChart, label: string, value: float, color: Color) =
            barChart.AddItem(label, value, color)

        [<CustomOperation "items">]
        member __.Items(barChart: BarChart, elements: 'T array, converter: 'T -> BarChartItem) =
            barChart.AddItems(elements, converter)

    let barChart = BarChartBuilder()
