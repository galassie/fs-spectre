namespace FsSpectre

open System
open System.Globalization
open Spectre.Console

[<AutoOpen>]
module CalendarBuilder =

    type CalendarConfig =
        { DateTime: DateTime
          CultureInfo: CultureInfo
          ShowHeader: bool
          HeaderStyle: Style
          HighlightStyle: Style
          Events: CalendarEvent array }

        static member Default(dateTime: DateTime) =
            { DateTime = dateTime
              CultureInfo = CultureInfo.CurrentCulture
              ShowHeader = true
              HeaderStyle = Style.Plain
              HighlightStyle = Style.Plain
              Events = Array.empty<CalendarEvent> }

    type CalendarBuilder() =
        member __.Yield _ = CalendarConfig.Default(DateTime.Now)

        member __.Run(config: CalendarConfig) =
            let result = Calendar(config.DateTime)
            result.Culture <- config.CultureInfo
            result.HeaderStyle <- config.HeaderStyle
            result.HightlightStyle <- config.HighlightStyle
            config.Events |> Array.map result.CalendarEvents.Add |> ignore
            result

        [<CustomOperation "default">]
        member __.Default(config: CalendarConfig) = config

        [<CustomOperation "date_time">]
        member __.DateTime(config: CalendarConfig, dateTime: DateTime) = { config with DateTime = dateTime }

        [<CustomOperation "culture">]
        member __.Culture(config: CalendarConfig, cultureInfo: CultureInfo) =
            { config with
                CultureInfo = cultureInfo }

        [<CustomOperation "header_style">]
        member __.HeaderStyle(config: CalendarConfig, style: Style) = { config with HeaderStyle = style }

        [<CustomOperation "highlight_style">]
        member __.HighlightStyle(config: CalendarConfig, style: Style) = { config with HighlightStyle = style }

        [<CustomOperation "hide_header">]
        member __.HideHeader(config: CalendarConfig) = { config with ShowHeader = false }

        [<CustomOperation "events">]
        member __.Events(config: CalendarConfig, events: CalendarEvent array) =
            { config with
                Events = Array.append config.Events events }

    let calendar = CalendarBuilder()
