package me.tomassetti.examples.MarkupParser;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.io.PrintStream;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import me.tomassetti.examples.MarkupParser.MarkupParser.FileContext;

public class MarkupVisitor extends MarkupParserBaseVisitor<String>
{            
    private PrintStream stream;
    public MarkupVisitor(PrintStream stream)
    {
        this.stream = stream;
    }

    @Override
    public String visitFile(MarkupParser.FileContext context)    
    {
        visitChildren(context);
           
        stream.println();

        return null;
    }

    @Override
    public String visitTag(MarkupParser.TagContext context)    
    {
        String text = "";
        String startDelimiter = "", endDelimiter = "";

        String id = context.ID(0).getText();
        
        switch(id)
        {
            case "b":
                startDelimiter = endDelimiter = "**";                
            break;
            case "u":
                startDelimiter = endDelimiter = "*";                
            break;
            case "quote":
                String attribute = context.attribute().STRING().getText();
                attribute = attribute.substring(1,attribute.length()-1);
                startDelimiter = System.lineSeparator() + "> ";
                endDelimiter = System.lineSeparator() + "> " + System.lineSeparator() + "> – "
                             + attribute + System.lineSeparator();
            break;
        } 

        text += startDelimiter;

        for (MarkupParser.ElementContext node: context.element())
        {                
            if(node.tag() != null)
                text += visitTag(node.tag());
            if(node.content() != null)
                text += visitContent(node.content());                
        }        
        
        text += endDelimiter;
        
        return text;        
    }

    @Override
    public String visitContent(MarkupParser.ContentContext context)    
    {          
        return context.getText();        
    }    

    @Override
    public String visitElement(MarkupParser.ElementContext context)
    {
        if(context.parent instanceof MarkupParser.FileContext)
        {
            if(context.content() != null)            
                stream.print(visitContent(context.content()));
                
            if(context.tag() != null)
                stream.print(visitTag(context.tag()));                
        }    

        return null;
    }
}