namespace FsSpectre

open Spectre.Console

[<AutoOpen>]
module RuleBuilder =

    type RuleBuilder() =
        member __.Yield _ = Rule()

        [<CustomOperation "empty">]
        member __.Empty(rule: Rule) = rule

        [<CustomOperation "title">]
        member __.Title(rule: Rule, title: string) =
            rule.Title <- title
            rule

        [<CustomOperation "justification">]
        member __.Justification(rule: Rule, justify: Justify) =
            rule.Justification <- justify
            rule

    let rule = RuleBuilder()
