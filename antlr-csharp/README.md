# ANTLR4 Tutorial - CSharp Project

If you need help to setup everything you can read the [CSharp Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#csharp-setup)

This example project is configured to work with Visual Studio Code, but can also work with Visual Studio.

With Visual Studio 2017 and the corresponding extension, everything is generated when you compile your program, so you do not need to use any specific command. We configured the project to also automatically generate the grammar with Visual Studio Code and the ANTLR4 grammar syntax support (mike-lischke.vscode-antlr4) extension. You can read the configuration values in `.vscode\settings.json`. 

Instead, if you you want to generate the parser and lexer directly with the ANTLR tool, you can do it just like this.

```
# to generate parser, lexer and visitor, with the right namespace
antlr4 -Dlanguage=CSharp Spreadsheet.g4 -no-listener -visitor -package AntlrTutorial
```

Notice that both extensions comes with their own internal ANTLR tool for ease of use. This way you do not need to have ANTLR installed in your system. However, this mean that the ANTLR version included might be outdated. You can check the version mentioned in the generated parser. The Visual Studio extension uses the custom ANTLR Sharwell tool, while the Visual Studio Code uses the ANTLR Standard tool. 

If you are using Visual Studio Code, or you want to use the command line, you can build the project as any other .NET Core/5 project.

```
# remember to generate the parser before building the project
# to build the project
dotnet build
# run the main project
dotnet run --project .\antlr-csharp\antlr-csharp.csproj
# to run the tests
dotnet test
```

## Picking the correct runtime (i.e. Nuget package)

You must also choose the correct runtime, depending on which extension you use, because they are not all the same. If you are using the Visual Studio Extension, use the older [ANTLR 4 Runtime](https://www.nuget.org/packages/Antlr4.Runtime/) by sharwell. In all other cases, that is if you use the Visual Studio Code extension or you do everything manually you must use the [ANTLR4 Standard Runtime](https://www.nuget.org/packages/Antlr4.Runtime.Standard/).


## Small differences

This example project uses the ANTLR4 Standard.

There are two small differences between the code for a project that uses the `ANTLR4 Sharwell` and one that uses the `ANTLR4 Standard`. The ANTLR4 standard generates a file `SpreadsheetVisitor.cs` containing the interface `ISpreadsheetVisitor`. This is why the name of the default visitor is `DoubleSpreadsheetVisitor.cs`. If you are using the ANTLR4 Sharwell, you may want to change the name of the visitor class into `SpreadsheetVisitor.cs`.

The signature for `SpreadsheetErrorListener.SyntaxError` is slightly different:

```
// if you are generating the parser with the Visual Studio ANTLR Extension by Sam Harwell
// public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
```

## Other useful links

If you are still stuck with setting things up, these links may help you:

- [SO answer](https://stackoverflow.com/a/23313015/290460) how to deal with generated files by antlr4 in Visual Studio
- [Windows ANTLR4 installation guideline](https://levlaz.org/setting-up-antlr4-on-windows/)
