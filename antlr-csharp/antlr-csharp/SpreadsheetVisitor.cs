using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace AntlrTutorial
{
    public class SpreadsheetVisitor : SpreadsheetBaseVisitor<double>
    {
        private static DataRepository data = new DataRepository();
        
        public override double VisitNumericAtomExp(SpreadsheetParser.NumericAtomExpContext context)
        {
            return double.Parse(context.NUMBER().GetText(), System.Globalization.CultureInfo.InvariantCulture);
        }

        public override double VisitIdAtomExp(SpreadsheetParser.IdAtomExpContext context)
        {
            String id = context.ID().GetText();

            return data[id];
        }

        public override double VisitParenthesisExp(SpreadsheetParser.ParenthesisExpContext context)
        {
            return Visit(context.expression());            
        }

        public override double VisitMulDivExp(SpreadsheetParser.MulDivExpContext context)
        {            
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));
            double result = 0;

            if (context.ASTERISK() != null)
                result = left * right;
            if (context.SLASH() != null)
                result = left / right;

            return result;
        }

        public override double VisitAddSubExp(SpreadsheetParser.AddSubExpContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));
            double result = 0;

            if (context.PLUS() != null)
                result = left + right;
            if (context.MINUS() != null)
                result = left - right;

            return result;
        }

        public override double VisitPowerExp(SpreadsheetParser.PowerExpContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));
            double result = 0;
           
            result = Math.Pow(left,right);            

            return result;
        }

        public override double VisitFunctionExp(SpreadsheetParser.FunctionExpContext context)
        {
            String name = context.NAME().GetText();
            double result = 0;

            switch(name)
            {
                case "sqrt":
                    result = Math.Sqrt(Visit(context.expression()));
                    break;

                case "log":
                    result = Math.Log10(Visit(context.expression()));
                    break;
            }

            return result;
        }
    }
}
