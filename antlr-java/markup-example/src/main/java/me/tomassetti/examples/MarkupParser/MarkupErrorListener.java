package me.tomassetti.examples.MarkupParser;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;

import java.io.PrintStream;
import java.io.StringWriter;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import me.tomassetti.examples.MarkupParser.MarkupParser.FileContext;
public class MarkupErrorListener extends BaseErrorListener
{            
    private String _symbol = "";
    private StringWriter _stream;
    public MarkupErrorListener(StringWriter stream)
    {
         this._stream = stream;
    }
    public String getSymbol() {
        return _symbol;
    } 

    public StringWriter getStream() {
        return _stream;
    }   
    
    @Override    
	public void syntaxError(Recognizer<?, ?> recognizer,
							Object offendingSymbol,
							int line,
							int charPositionInLine,
							String msg,
							RecognitionException e)
	{
        this._stream.write(msg);
        this._stream.write(System.getProperty("line.separator"));            

        if(offendingSymbol.getClass().getName() == "org.antlr.v4.runtime.CommonToken")
        {            
            CommonToken token = (CommonToken) offendingSymbol;
            this._symbol = token.getText();            
        }                
    };	
}