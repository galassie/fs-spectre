namespace FsSpectre

open System
open System.Threading.Tasks
open Spectre.Console

[<AutoOpen>]
module ProgressBuilder =

    type ProgressConfig<'T> =
        { AutoRefresh: bool
          AutoClear: bool
          HideCompleted: bool
          Columns: ProgressColumn array
          Start: ProgressContext -> 'T }

        static member Default =
            { AutoRefresh = true
              AutoClear = false
              HideCompleted = false
              Columns = [| TaskDescriptionColumn(); ProgressBarColumn(); PercentageColumn() |]
              Start = fun _ctx -> Unchecked.defaultof<'T> }

    type ProgressBuilder<'T>() =
        member __.Yield _ = ProgressConfig<'T>.Default

        member __.Run(config: ProgressConfig<'T>) =
            let progress = AnsiConsole.Progress().Columns(config.Columns)
            progress.AutoRefresh <- config.AutoRefresh
            progress.AutoClear <- config.AutoClear
            progress.HideCompleted <- config.HideCompleted

            progress.Start(config.Start)

        [<CustomOperation "auto_refresh">]
        member __.AutoRefresh(config: ProgressConfig<'T>, autoRefresh: bool) = { config with AutoRefresh = autoRefresh }

        [<CustomOperation "auto_clear">]
        member __.AutoClear(config: ProgressConfig<'T>, autoClear: bool) = { config with AutoClear = autoClear }

        [<CustomOperation "hide_completed">]
        member __.HideCompleted(config: ProgressConfig<'T>, hideCompleted: bool) =
            { config with
                HideCompleted = hideCompleted }

        [<CustomOperation "columns">]
        member __.Columns(config: ProgressConfig<'T>, columns: ProgressColumn array) =
            { config with
                Columns = columns }

        [<CustomOperation "start">]
        member __.Start(config: ProgressConfig<'T>, start: ProgressContext -> 'T) = { config with Start = start }

    type ProgressAsyncConfig<'T> =
        { AutoRefresh: bool
          AutoClear: bool
          HideCompleted: bool
          Columns: ProgressColumn array
          Start: ProgressContext -> Task<'T> }

        static member Default =
            { AutoRefresh = true
              AutoClear = false
              HideCompleted = false
              Columns = [| TaskDescriptionColumn(); ProgressBarColumn(); PercentageColumn() |]
              Start = fun _ctx -> Task.FromResult(Unchecked.defaultof<'T>) }

    type ProgressAsyncBuilder<'T>() =
        member __.Yield _ = ProgressAsyncConfig<'T>.Default

        member __.Run(config: ProgressAsyncConfig<'T>) =
            let progress = AnsiConsole.Progress().Columns(config.Columns)
            progress.AutoRefresh <- config.AutoRefresh
            progress.AutoClear <- config.AutoClear
            progress.HideCompleted <- config.HideCompleted

            progress.StartAsync(config.Start)

        [<CustomOperation "auto_refresh">]
        member __.AutoRefresh(config: ProgressAsyncConfig<'T>, autoRefresh: bool) =
            { config with AutoRefresh = autoRefresh }

        [<CustomOperation "auto_clear">]
        member __.AutoClear(config: ProgressAsyncConfig<'T>, autoClear: bool) = { config with AutoClear = autoClear }

        [<CustomOperation "hide_completed">]
        member __.HideCompleted(config: ProgressAsyncConfig<'T>, hideCompleted: bool) =
            { config with
                HideCompleted = hideCompleted }

        [<CustomOperation "columns">]
        member __.Columns(config: ProgressAsyncConfig<'T>, columns: ProgressColumn array) =
            { config with
                Columns = columns }

        [<CustomOperation "start">]
        member __.Start(config: ProgressAsyncConfig<'T>, start: ProgressContext -> Task<'T>) =
            { config with Start = start }

    let progress<'T> = ProgressBuilder<'T>()
    let progressAsync<'T> = ProgressAsyncBuilder<'T>()
