package me.tomassetti.examples.MarkupParser;

import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.tree.*;

import java.io.StringWriter;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

/**
 * Unit test for simple App.
 */
public class AppTest extends TestCase
{
    /**
     * Create the test case
     *
     * @param testName name of the test case
     */
    public AppTest( String testName )
    {
        super( testName );
    }

    /**
     * @return the suite of tests being tested
     */
    public static Test suite()
    {
        return new TestSuite( AppTest.class );
    }            
    
    private MarkupParser setup(String input)
    {            
        ANTLRInputStream inputStream = new ANTLRInputStream(input);
        this.markupLexer = new MarkupLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(markupLexer);
        MarkupParser markupParser = new MarkupParser(commonTokenStream);
                
        StringWriter writer = new StringWriter();
        this.errorListener = new MarkupErrorListener(writer);
        markupLexer.removeErrorListeners();
        //markupLexer.addErrorListener(errorListener);
        markupParser.removeErrorListeners();
        markupParser.addErrorListener(errorListener);

        return markupParser;
    }

    private MarkupErrorListener errorListener;
    private MarkupLexer markupLexer;

    public void testText()
    {
        MarkupParser parser = setup("anything in here");

        MarkupParser.ContentContext context = parser.content();        
        
        assertEquals("",this.errorListener.getSymbol());
    }

    public void testInvalidText()
    {
        MarkupParser parser = setup("[anything in here");

        MarkupParser.ContentContext context = parser.content();        
        
        // note that this.errorListener.symbol could be empty
        // when ANTLR doesn't recognize the token or there is no error.           
        // In such cases check the output of errorListener        
        assertEquals("[",this.errorListener.getSymbol());
    }

    public void testWrongMode()
    {
        MarkupParser parser = setup("author=\"john\"");                

        MarkupParser.AttributeContext context = parser.attribute(); 
        TokenStream ts = parser.getTokenStream();        
        
        assertEquals(MarkupLexer.DEFAULT_MODE, markupLexer._mode);
        assertEquals(MarkupLexer.TEXT,ts.get(0).getType());        
        assertEquals("author=\"john\"",this.errorListener.getSymbol());
    }

    public void testAttribute()
    {
        MarkupParser parser = setup("author=\"john\"");
        // we have to manually push the correct mode
        this.markupLexer.pushMode(MarkupLexer.BBCODE);

        MarkupParser.AttributeContext context = parser.attribute(); 
        TokenStream ts = parser.getTokenStream();        
        
        assertEquals(MarkupLexer.ID,ts.get(0).getType());
        assertEquals(MarkupLexer.EQUALS,ts.get(1).getType());
        assertEquals(MarkupLexer.STRING,ts.get(2).getType()); 
        
        assertEquals("",this.errorListener.getSymbol());
    }

    public void testInvalidAttribute()
    {
        MarkupParser parser = setup("author=/\"john\"");
        // we have to manually push the correct mode
        this.markupLexer.pushMode(MarkupLexer.BBCODE);
        
        MarkupParser.AttributeContext context = parser.attribute();        
        
        assertEquals("/",this.errorListener.getSymbol());
    }
}
