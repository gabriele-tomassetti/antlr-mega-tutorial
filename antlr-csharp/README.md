# ANTLR4 Tutorial - CSharp Project

If you need help to setup everything you can read the [CSharp Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#csharp-setup)

With Visual Studio everything is generated when you compile your program, so you don't need to use any command. Unless you want generate the parser and lexer with the ANTLR tool.

```
// to generate parser and lexer
antlr4 -Dlanguage=CSharp Spreadsheet.g4 
```