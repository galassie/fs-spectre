namespace FsSpectre

open System
open System.Threading.Tasks
open Spectre.Console

[<AutoOpen>]
module StatusBuilder =

    type StatusConfig<'T> =
        { AutoRefresh: bool
          Spinner: Spinner
          SpinnerStyle: Style
          Status: string
          Start: StatusContext -> 'T }

        static member Default =
            { AutoRefresh = true
              Spinner = Spinner.Known.Default
              SpinnerStyle = new Style(Color.Yellow)
              Status = String.Empty
              Start = fun _ctx -> Unchecked.defaultof<'T> }

    type StatusBuilder<'T>() =
        member __.Yield _ = StatusConfig<'T>.Default

        member __.Run(config: StatusConfig<'T>) =
            let status = AnsiConsole.Status()
            status.AutoRefresh <- config.AutoRefresh
            status.Spinner <- config.Spinner
            status.SpinnerStyle <- config.SpinnerStyle

            status.Start(config.Status, config.Start)

        [<CustomOperation "auto_refresh">]
        member __.AutoRefresh(config: StatusConfig<'T>, autoRefresh: bool) = { config with AutoRefresh = autoRefresh }

        [<CustomOperation "spinner">]
        member __.Spinner(config: StatusConfig<'T>, spinner: Spinner) = { config with Spinner = spinner }

        [<CustomOperation "spinner_style">]
        member __.SpinnerStyle(config: StatusConfig<'T>, spinnerStyle: Style) =
            { config with
                SpinnerStyle = spinnerStyle }

        [<CustomOperation "status">]
        member __.Status(config: StatusConfig<'T>, status: string) = { config with Status = status }

        [<CustomOperation "start">]
        member __.Start(config: StatusConfig<'T>, start: StatusContext -> 'T) = { config with Start = start }

    type StatusAsyncConfig<'T> =
        { AutoRefresh: bool
          Spinner: Spinner
          SpinnerStyle: Style
          Status: string
          Start: StatusContext -> Task<'T> }

        static member Default =
            { AutoRefresh = true
              Spinner = Spinner.Known.Default
              SpinnerStyle = new Style(Color.Yellow)
              Status = String.Empty
              Start = fun _ctx -> Task.FromResult(Unchecked.defaultof<'T>) }

    type StatusAsyncBuilder<'T>() =
        member __.Yield _ = StatusAsyncConfig<'T>.Default

        member __.Run(config: StatusAsyncConfig<'T>) =
            let status = AnsiConsole.Status()
            status.AutoRefresh <- config.AutoRefresh
            status.Spinner <- config.Spinner
            status.SpinnerStyle <- config.SpinnerStyle

            status.StartAsync(config.Status, config.Start)

        [<CustomOperation "auto_refresh">]
        member __.AutoRefresh(config: StatusAsyncConfig<'T>, autoRefresh: bool) = { config with AutoRefresh = autoRefresh }

        [<CustomOperation "spinner">]
        member __.Spinner(config: StatusAsyncConfig<'T>, spinner: Spinner) = { config with Spinner = spinner }

        [<CustomOperation "spinner_style">]
        member __.SpinnerStyle(config: StatusAsyncConfig<'T>, spinnerStyle: Style) =
            { config with
                SpinnerStyle = spinnerStyle }

        [<CustomOperation "status">]
        member __.Status(config: StatusAsyncConfig<'T>, status: string) = { config with Status = status }

        [<CustomOperation "start">]
        member __.Start(config: StatusAsyncConfig<'T>, start: StatusContext -> Task<'T>) = { config with Start = start }

    let status<'T> = StatusBuilder<'T>()
    let statusAsync<'T> = StatusAsyncBuilder<'T>()
