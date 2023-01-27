#r "nuget: Spectre.Console, 0.46.0"
#r "src/FsSpectre/bin/Debug/net6.0/FsSpectre.dll"

open Spectre.Console
open FsSpectre

table {
    title_text "[u][yellow]Spectre.Console[/] [b]Features[/][/]"
    column (tableColumn {
        header "Feature"
        no_wrap
        right_aligned
        width 10
        pad_right 3
    })

    column (tableColumn {
        header "Demonstration"
        pad_right 0
    })

    empty_row
} |> AnsiConsole.Write