#r "nuget: Spectre.Console, 0.46.0"
#r "src/FsSpectre/bin/Debug/net6.0/FsSpectre.dll"

open Spectre.Console
open FsSpectre

table {
    title_text "[u][yellow]Spectre.Console[/] [b]Features[/][/]"

    column (
        tableColumn {
            header "Feature"
            no_wrap
            right_aligned
            width 10
            pad_right 3
        }
    )

    column (
        tableColumn {
            header "Demonstration"
            pad_right 0
        }
    )

    empty_row
    row [| 
        markup { text "[red]OS[/]" }
        grid {
            expand
            number_of_columns 3
            row_text [|
                "[bold green]Windows[/]"
                "[bold blue]macOS[/]"
                "[bold yellow]Linux[/]"
            |]
        }
    |]

    empty_row
    row_text
        [| "[red]Styles[/]"
           "All ansi styles: [bold]bold[/], [dim]dim[/], [italic]italic[/], [underline]underline[/], [strikethrough]strikethrough[/], [reverse]reverse[/], and even [blink]blink[/]." |]

    empty_row
    row [|
        markup { text "[red]Text[/]" }
        markup { text "Word wrap text. Justify [green]left[/], [yellow]center[/] or [blue]right[/]." }
    |]
    
    empty_row
    row [|
        text { empty }
        grid { 
            column (gridColumn { left_aligned })
            column (gridColumn { centerd })
            column (gridColumn { right_aligned })
            row_text [|
                "[green]Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque in metus sed sapien ultricies pretium a at justo. Maecenas luctus velit et auctor maximus.[/]"
                "[yellow]Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque in metus sed sapien ultricies pretium a at justo. Maecenas luctus velit et auctor maximus.[/]"
                "[blue]Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque in metus sed sapien ultricies pretium a at justo. Maecenas luctus velit et auctor maximus.[/]"
            |]
        }
    |]

    empty_row
    row [|
        markup { text "[red]Tables and Trees[/]" }
        markup { text "Word wrap text. Justify [green]left[/], [yellow]center[/] or [blue]right[/]." }
    |]
}
|> AnsiConsole.Write
