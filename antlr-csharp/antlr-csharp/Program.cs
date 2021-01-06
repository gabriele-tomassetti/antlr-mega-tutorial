﻿using System;
using Antlr4.Runtime;

namespace AntlrTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "log(10 + A1 * 35 + (5.4 - 7.4))";

            ICharStream inputStream = CharStreams.fromString(input);
            SpreadsheetLexer spreadsheetLexer = new SpreadsheetLexer(inputStream);            
            CommonTokenStream commonTokenStream = new CommonTokenStream(spreadsheetLexer);
            SpreadsheetParser spreadsheetParser = new SpreadsheetParser(commonTokenStream);

            SpreadsheetParser.ExpressionContext expressionContext = spreadsheetParser.expression();
            DoubleSpreadsheetVisitor visitor = new DoubleSpreadsheetVisitor();

            Console.WriteLine(visitor.Visit(expressionContext));
        }
    }    
}
