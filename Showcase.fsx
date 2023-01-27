#r "nuget: Spectre.Console, 0.46.0"
#r "src/FsSpectre/bin/Debug/net6.0/FsSpectre.dll"

open System
open Spectre.Console
open FsSpectre

let generateException () = try raise (InvalidOperationException("This is invalid!")) with | ex -> ex

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
        markup { text "[red]Markup[/]" }
        markup { text "[bold purple]Spectre.Console[/] supports a simple [i]bbcode[/] like [b]markup[/] for [yellow]color[/], [underline]style[/], and emoji! :thumbs_up: :red_apple: :ant: :bear: :baguette_bread: :bus:" }
    |]

    empty_row
    row [|
        markup { text "[red]Tables and Trees[/]" }
        table {
            rounded_border
            collapse
            border_color Color.Yellow
            columns_text [| "Foo"; "Bar" |]
            row [|
                text { text "Baz" }
                table {
                    simple_border
                    border_color Color.Grey
                    columns [|
                        tableColumn { header "Overview" }
                        tableColumn { footer "[grey]3 Files, 225 KiB[/]" }
                    |]

                    row [|
                        markup { text "[yellow]Files[/]" }
                        tree {
                            label "📁 src"
                            node (treeNode 
                                { 
                                    label "📁 foo" 
                                    node (treeNode { label "📄 bar.cs" } )
                                })
                            node (treeNode 
                                { 
                                    label "📁 baz" 
                                    node (treeNode 
                                        { 
                                            label "📁 qux" 
                                            node (treeNode { label "📄 corgi.txt" } )
                                        } )
                                })
                            node (treeNode { label "📄 waldo.xml" })
                        }
                    |]
                }
            |]
            row_text [|
                "Qux"
                "Corgi"
            |]
        }
    |]

    empty_row
    row [|
        markup { text "[red]Tables and Trees[/]" }
        grid {
            collapse
            number_of_columns 2
            row [|
                panel {
                    border_color Color.Grey
                    content_renderable (breakdownChart {
                        show_percentage
                        full_size
                        item "F#" 82 Color.Violet
                        item "PowerShell" 13 Color.Red
                        item "Bash" 5 Color.Blue
                    })
                }
                panel {
                    border_color Color.Grey
                    content_renderable (barChart {
                        item "Apple" 32 Color.Green
                        item "Oranges" 13 Color.Orange1
                        item "Bananas" 22 Color.Yellow
                    })
                }
            |]
        }
    |]

    empty_row
    row [|
        markup { text "[red]Exceptions[/]" }
        generateException().GetRenderable()
    |]

    empty_row
    row_text [|
        "[red]+ Much more![/]"
        "Tables, Grids, Trees, Progress bars, Status, Bar charts, Calendars, Figlet, Images, Text prompts, List boxes, Separators, Pretty exceptions, Canvas, CLI parsing"
    |]
    empty_row
}
|> AnsiConsole.Write
