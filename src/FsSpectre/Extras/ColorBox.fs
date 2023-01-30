namespace FsSpectre

open System
open Spectre.Console
open Spectre.Console.Rendering


type ColorBox(height: int, ?width: int) =
    inherit Renderable()

    override this.Measure(_, maxWidth: int) = Measurement(1, this.GetWidth(maxWidth))

    override this.Render(_, maxWidth: int) =
        let actualWidth = this.GetWidth(maxWidth)

        seq {
            for y in 1..height do
                for x in 1..actualWidth do
                    let h = (double x) / (double actualWidth)
                    let lBackground = (double 0.1) + (((double y) / (double height)) * 0.7)
                    let lForeground = lBackground + (0.7 / 10.0)
                    let s = (double 1.0)

                    let (r1, g1, b1) = ColorBox.ColorFromHSL(h, lBackground, s)
                    let (r2, g2, b2) = ColorBox.ColorFromHSL(h, lForeground, s)

                    let background = Color(byte (r1 * 255.0), byte (g1 * 255.0), byte (b1 * 255.0))
                    let foreground = Color(byte (r2 * 255.0), byte (g2 * 255.0), byte (b2 * 255.0))

                    Segment("▄", Style(foreground, background))

                Segment.LineBreak
        }

    member private __.GetWidth(maxWidth: int) =
        match width with
        | None -> maxWidth
        | Some w -> Math.Min(w, maxWidth)

    static member private ColorFromHSL(h: double, l: double, s: double) =
        if l = 0.0 then
            (0.0, 0.0, 0.0)

        elif s = 0.0 then
            ((float l), (float l), (float l))
        else
            let temp2 = if l < 0.5 then l * (1.0 + s) else l + s - (l * s)
            let temp1 = 2.0 * l - temp2

            (ColorBox.GetColorComponent(temp1, temp2, h + 1.0 / 3.0),
             ColorBox.GetColorComponent(temp1, temp2, h),
             ColorBox.GetColorComponent(temp1, temp2, h - 1.0 / 3.0))

    static member private GetColorComponent(temp1: double, temp2: double, temp3: double) =
        let actualTemp3 =
            if temp3 < 0.0 then temp3 + 1.0
            elif temp3 > 1.0 then temp3 - 1.0
            else temp3

        if actualTemp3 < 1.0 / 6.0 then
            temp1 + (temp2 - temp1) * 6.0 * actualTemp3
        elif actualTemp3 < 0.5 then
            temp2
        elif actualTemp3 < 2.0 / 3.0 then
            temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - actualTemp3) * 6.0)
        else
            temp1
