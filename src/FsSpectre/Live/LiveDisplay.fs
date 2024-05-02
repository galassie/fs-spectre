namespace FsSpectre

open System.Threading.Tasks
open Spectre.Console
open Spectre.Console.Rendering

[<AutoOpen>]
module LiveDisplayBuilder =

    type LiveDisplayConfig<'T> =
        { Target: IRenderable option
          AutoClear: bool
          Overflow: VerticalOverflow
          Cropping: VerticalOverflowCropping
          Start: LiveDisplayContext -> 'T }

        static member Default =
            { Target = None
              AutoClear = false
              Overflow = VerticalOverflow.Ellipsis
              Cropping = VerticalOverflowCropping.Top
              Start = fun _ctx -> Unchecked.defaultof<'T> }

    type LiveDisplayBuilder<'T>() =
        member __.Yield _ = LiveDisplayConfig<'T>.Default

        member __.Run(config: LiveDisplayConfig<'T>) =
            let liveDisplay = AnsiConsole.Live(Option.toObj config.Target)
            liveDisplay.AutoClear <- config.AutoClear
            liveDisplay.Overflow <- config.Overflow
            liveDisplay.Cropping <- config.Cropping

            liveDisplay.Start(config.Start)

        [<CustomOperation "target">]
        member __.Target(config: LiveDisplayConfig<'T>, target: IRenderable) = { config with Target = Some(target) }

        [<CustomOperation "auto_clear">]
        member __.AutoClear(config: LiveDisplayConfig<'T>, autoClear: bool) = { config with AutoClear = autoClear }

        [<CustomOperation "overflow">]
        member __.Overflow(config: LiveDisplayConfig<'T>, overflow: VerticalOverflow) =
            { config with Overflow = overflow }

        [<CustomOperation "cropping">]
        member __.Cropping(config: LiveDisplayConfig<'T>, cropping: VerticalOverflowCropping) =
            { config with Cropping = cropping }

        [<CustomOperation "start">]
        member __.Start(config: LiveDisplayConfig<'T>, start: LiveDisplayContext -> 'T) = { config with Start = start }

    type LiveDisplayAsyncConfig<'T> =
        { Target: IRenderable option
          AutoClear: bool
          Overflow: VerticalOverflow
          Cropping: VerticalOverflowCropping
          Start: LiveDisplayContext -> Task<'T> }

        static member Default =
            { Target = None
              AutoClear = false
              Overflow = VerticalOverflow.Ellipsis
              Cropping = VerticalOverflowCropping.Top
              Start = fun _ctx -> Task.FromResult(Unchecked.defaultof<'T>) }

    type LiveDisplayAsyncBuilder<'T>() =
        member __.Yield _ = LiveDisplayAsyncConfig<'T>.Default

        member __.Run(config: LiveDisplayAsyncConfig<'T>) =
            let liveDisplay = AnsiConsole.Live(Option.toObj config.Target)
            liveDisplay.AutoClear <- config.AutoClear
            liveDisplay.Overflow <- config.Overflow
            liveDisplay.Cropping <- config.Cropping

            liveDisplay.StartAsync(config.Start)

        [<CustomOperation "target">]
        member __.Target(config: LiveDisplayAsyncConfig<'T>, target: IRenderable) = { config with Target = Some(target) }

        [<CustomOperation "auto_clear">]
        member __.AutoClear(config: LiveDisplayAsyncConfig<'T>, autoClear: bool) = { config with AutoClear = autoClear }

        [<CustomOperation "overflow">]
        member __.Overflow(config: LiveDisplayAsyncConfig<'T>, overflow: VerticalOverflow) =
            { config with Overflow = overflow }

        [<CustomOperation "cropping">]
        member __.Cropping(config: LiveDisplayAsyncConfig<'T>, cropping: VerticalOverflowCropping) =
            { config with Cropping = cropping }

        [<CustomOperation "start">]
        member __.Start(config: LiveDisplayAsyncConfig<'T>, start: LiveDisplayContext -> Task<'T>) =
            { config with Start = start }

    let liveDisplay<'T> = LiveDisplayBuilder<'T>()
    let liveDisplayAsync<'T> = LiveDisplayAsyncBuilder<'T>()
