namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module CalendarEventBuilder =

    type CalendarEventBuilder() =
        member __.Yield _ =
            let now = DateTime.Now
            CalendarEvent(now.Year, now.Month, now.Day)

        [<CustomOperation "year">]
        member __.Year(calendarEvent: CalendarEvent, year: int) =
            CalendarEvent(calendarEvent.Description, year, calendarEvent.Month, calendarEvent.Day)

        [<CustomOperation "month">]
        member __.Month(calendarEvent: CalendarEvent, month: int) =
            CalendarEvent(calendarEvent.Description, calendarEvent.Year, month, calendarEvent.Day)

        [<CustomOperation "day">]
        member __.Day(calendarEvent: CalendarEvent, day: int) =
            CalendarEvent(calendarEvent.Description, calendarEvent.Year, calendarEvent.Month, day)

        [<CustomOperation "year_month_day">]
        member __.Year(calendarEvent: CalendarEvent, year: int, month: int, day: int) =
            CalendarEvent(calendarEvent.Description, year, month, day)

        [<CustomOperation "description">]
        member __.Description(calendarEvent: CalendarEvent, description: string) =
            CalendarEvent(description, calendarEvent.Year, calendarEvent.Month, calendarEvent.Day)

    let calendarEvent = CalendarEventBuilder()
