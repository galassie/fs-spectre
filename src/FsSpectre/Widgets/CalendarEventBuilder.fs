namespace FsSpectre

open System
open Spectre.Console

[<AutoOpen>]
module CalendarEventBuilder =

    type CalendarEventConfig =
        { DateTime: DateTime
          Description: string }

        static member Default(dateTime: DateTime) =
            { DateTime = dateTime
              Description = String.Empty }

    type CalendarEventBuilder() =
        member __.Yield _ =
            CalendarEventConfig.Default(DateTime.Now)

        member __.Run(config: CalendarEventConfig) =
            CalendarEvent(config.Description, config.DateTime.Year, config.DateTime.Month, config.DateTime.Day)

        [<CustomOperation "default">]
        member __.Default(config: CalendarEventConfig) = config

        [<CustomOperation "date_time">]
        member __.Year(config: CalendarEventConfig, dateTime: DateTime) = { config with DateTime = dateTime }

        [<CustomOperation "description">]
        member __.Description(config: CalendarEventConfig, description: string) =
            { config with
                Description = description }

    let calendarEvent = CalendarEventBuilder()
