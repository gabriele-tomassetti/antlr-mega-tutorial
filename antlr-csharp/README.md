# ANTLR4 Tutorial - CSharp Project

If you need help to setup everything you can read the [CSharp Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#csharp-setup)

With Visual Studio 2017, and the proper extension, everything is generated when you compile your program, so you don't need to use any specific command.

Instead, if you you want generate the parser and lexer with the ANTLR tool, you can do it just like this.

```
// to generate parser, lexer and visitor, with the right namespace
antlr4 -Dlanguage=CSharp Spreadsheet.g4 -no-listener -visitor -package AntlrTutorial
```

If you are using the ANTLR tool you must also choose the correct runtime, because  they are not all the same. You must use the [ANTLR4 Standard Runtime](https://www.nuget.org/packages/Antlr4.Runtime.Standard/). Otherwise, if you are using the Visual Studio Extension, use the older [ANTLR 4 Runtime](https://www.nuget.org/packages/Antlr4.Runtime/) by sharwell.