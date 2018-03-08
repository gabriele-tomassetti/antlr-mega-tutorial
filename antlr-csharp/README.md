# ANTLR4 Tutorial - CSharp Project

If you need help to setup everything you can read the [CSharp Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#csharp-setup)

With Visual Studio 2015, and the proper extension, everything is generated when you compile your program, so you don't need to use any specific command.

Instead, if you you want generate the parser and lexer with the ANTLR tool, you can do it just like this.

```
// to generate parser, lexer and visitor, with the right namespace
antlr4 -Dlanguage=CSharp Spreadsheet.g4 -no-listener -visitor -package AntlrTutorial
```

If you are using the ANTLR tool you must also choose the correct runtime, because  they are not all the same. You must use the [ANTLR4 Standard Runtime](https://www.nuget.org/packages/Antlr4.Runtime.Standard/). Otherwise, if you are using the Visual Studio Extension, use the more popular [ANTLR 4 Runtime](https://www.nuget.org/packages/Antlr4.Runtime/) by sharwell.

#### Small differences

There are two small differences between the code for a project that uses the extension and one that uses the Java tool. First you may want to change the name of the file `SpreadsheetVisitor.cs` since it conflicts with a generated one. Even though the generated one is actually an interface. So you don't need to change the name of the class inside `SpreadsheetVisitor.cs`, just the name of the file.

The signature for `SpreadsheetErrorListener.SyntaxError` is also slightly different:

```
// if you are generating the parser with the Java tool change the signature to the following
public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
```

#### Other useful links

If you are still stuck with setting things up, these links may save you a tons of time:

- [SO answer](https://stackoverflow.com/a/23313015/290460) how to deal with generated files by antlr4 in Visual Studio
- [Windows ANTLR4 installation guideline](https://levlaz.org/setting-up-antlr4-on-windows/)
