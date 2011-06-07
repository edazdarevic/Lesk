## How to use Lesk?
Lesk is designed to be very simple to use. Regex is used for defining tokens. For example: 

`var lesk = new Lesk();`

`lesk.Add("[0-9]+", () => new NumberToken(), Lesk.AboveNormalPriority);`

`lesk.Add(":ap", () => new CommandToken());`

`lesk.Add(@"\s+", () => new WhitespaceToken());`

## What is a lexer anyway?

