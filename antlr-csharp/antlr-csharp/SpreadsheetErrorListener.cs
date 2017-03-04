using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using System.IO;

namespace AntlrTutorial
{    
    public class SpreadsheetErrorListener : BaseErrorListener
    {                
        public String Symbol { get; private set; }
        public StringWriter Writer { get; private set; }

        public SpreadsheetErrorListener(StringWriter writer)
        {
            Writer = writer;
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Writer.WriteLine(msg);

            Symbol = offendingSymbol.Text;
        }
    }
}
