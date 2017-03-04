package me.tomassetti.examples.MarkupParser;
import java.io.PrintWriter;

import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.tree.*;

public class App 
{
    public static void main( String[] args )
    {
        ANTLRInputStream inputStream = new ANTLRInputStream(
            "I would like to [b]emphasize[/b] this and [u]underline [b]that[/b][/u]. " +
            "Let's not forget to quote: [quote author=\"John\"]You're wrong![/quote]");
        MarkupLexer markupLexer = new MarkupLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(markupLexer);
        MarkupParser markupParser = new MarkupParser(commonTokenStream);

        MarkupParser.FileContext fileContext = markupParser.file();                
        MarkupVisitor visitor = new MarkupVisitor(System.out);                
        visitor.visit(fileContext);        
    }
}
