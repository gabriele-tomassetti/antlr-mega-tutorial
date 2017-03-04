using System;
using System.Linq;
using Xunit;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using System.IO;
using static AntlrTutorial.SpreadsheetParser;
using Xunit.Abstractions;

namespace AntlrTutorial
{    
    public class SpreadsheetTests
    {
        private SpreadsheetParser parser;
        private SpreadsheetLexer lexer;
        private SpreadsheetErrorListener errorListener;

        // if you need to output something during tests
        // private readonly ITestOutputHelper output;
        // 
        // public SpreadsheetTests(ITestOutputHelper output)
        // {
        //     this.output = output;
        // }

        private void setup(String input)
        {
            AntlrInputStream inputStream = new AntlrInputStream(input);
            lexer = new SpreadsheetLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            parser = new SpreadsheetParser(commonTokenStream);            

            StringWriter writer = new StringWriter();
            errorListener = new SpreadsheetErrorListener(writer);
            lexer.RemoveErrorListeners();
            //markupLexer.addErrorListener(errorListener);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);            
        }

        [Theory]
        [InlineData("1")]
        [InlineData("10")]
        [InlineData("10.00")]        
        public void testNumericAtomId(string value)
        {
            setup(value);

            IdAtomExpContext context = parser.expression() as IdAtomExpContext;

            CommonTokenStream ts = (CommonTokenStream)parser.InputStream;            

            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(0).Type);

            // note that this.errorListener.symbol could be null or empty
            // when ANTLR doesn't recognize the token or there is no error.
            // In such cases check the output of errorListener            
            Assert.Equal(null, errorListener.Symbol);
        }

        [Fact]
        public void testExpressionAtomId()
        {
            setup("A1");

            IdAtomExpContext context = parser.expression() as IdAtomExpContext;

            CommonTokenStream ts = (CommonTokenStream) parser.InputStream;            

            Assert.Equal(SpreadsheetLexer.ID, ts.Get(0).Type);           
            Assert.Equal(null, errorListener.Symbol);
        }

        [Fact]
        public void testWrongExpressionAtomId()
        {
            setup("AB1");

            IdAtomExpContext context = parser.expression() as IdAtomExpContext;

            CommonTokenStream ts = (CommonTokenStream)parser.InputStream;
            ts.Seek(0);

            Assert.Equal(SpreadsheetLexer.NAME, ts.Get(0).Type);
            Assert.Equal("<EOF>", errorListener.Symbol);
        }

        [Fact]
        public void testExpressionPow()
        {
            setup("5^3^2");

            PowerExpContext context = parser.expression() as PowerExpContext;

            CommonTokenStream ts = (CommonTokenStream)parser.InputStream;   

            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(0).Type);
            Assert.Equal(SpreadsheetLexer.T__2, ts.Get(1).Type);
            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(2).Type);
            Assert.Equal(SpreadsheetLexer.T__2, ts.Get(3).Type);
            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(4).Type); 
        }

        [Fact]
        public void testVisitPowerExp()
        {
            setup("4^3^2");

            PowerExpContext context = parser.expression() as PowerExpContext;

            SpreadsheetVisitor visitor = new SpreadsheetVisitor();
            double result = visitor.VisitPowerExp(context);

            Assert.Equal(double.Parse("262144"), result);
        }

        [Fact]
        public void testVisitFunctionExp()
        {
            setup("log(100)");

            FunctionExpContext context = parser.expression() as FunctionExpContext;

            SpreadsheetVisitor visitor = new SpreadsheetVisitor();
            double result = visitor.VisitFunctionExp(context);

            Assert.Equal(result, double.Parse("2"));
        }

        [Fact]
        public void testWrongVisitFunctionExp()
        {
            setup("logga(100)");

            FunctionExpContext context = parser.expression() as FunctionExpContext;

            SpreadsheetVisitor visitor = new SpreadsheetVisitor();
            double result = visitor.VisitFunctionExp(context);

            CommonTokenStream ts = (CommonTokenStream)parser.InputStream;

            // this is syntactically correct and would be true even for a good function
            Assert.Equal(SpreadsheetLexer.NAME, ts.Get(0).Type);
            Assert.Equal(null, errorListener.Symbol);
            // we choose to return 0 if we can't the find function with that NAME
            // so that's how we know is wrong
            Assert.Equal(0, result);
        }

        [Fact]
        public void testCompleteExp()
        {
            setup("log(5+6*7/8)");

            ExpressionContext context = parser.expression();

            SpreadsheetVisitor visitor = new SpreadsheetVisitor();
            double result = visitor.Visit(context);            

            Assert.Equal("1.01072386539177", result.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat));            
        }
    }
}
