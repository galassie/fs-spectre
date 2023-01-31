namespace FsSpectre

open System
open System.Globalization
open Spectre.Console

[<AutoOpen>]
module CalendarBuilder =

    type CalendarBuilder() =
        member __.Yield _ = Calendar(DateTime.Now)

        [<CustomOperation "year">]
        member __.Year(calendar: Calendar, year: int) =
            calendar.Year <- year
            calendar

        [<CustomOperation "month">]
        member __.Month(calendar: Calendar, month: int) =
            calendar.Month <- month
            calendar

        [<CustomOperation "day">]
        member __.Day(calendar: Calendar, day: int) =
            calendar.Day <- day
            calendar

        [<CustomOperation "culture">]
        member __.Culture(calendar: Calendar, cultureInfo: CultureInfo) =
            calendar.Culture <- cultureInfo
            calendar

        [<CustomOperation "hide_header">]
        member __.HideHeader(calendar: Calendar) = calendar.HideHeader()

        [<CustomOperation "event">]
        member __.Event(calendar: Calendar, event: CalendarEvent) =
            calendar.CalendarEvents.Add(event)
            calendar

        [<CustomOperation "events">]
        member __.Events(calendar: Calendar, events: CalendarEvent array) =
            events |> Array.map calendar.CalendarEvents.Add |> ignore
            calendar

    let calendar = CalendarBuilder()
