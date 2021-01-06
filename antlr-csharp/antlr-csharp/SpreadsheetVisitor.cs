//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Spreadsheet.g4 by ANTLR 4.9

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AntlrTutorial {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="SpreadsheetParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9")]
[System.CLSCompliant(false)]
public interface ISpreadsheetVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>numericAtomExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericAtomExp([NotNull] SpreadsheetParser.NumericAtomExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>powerExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPowerExp([NotNull] SpreadsheetParser.PowerExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>mulDivExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMulDivExp([NotNull] SpreadsheetParser.MulDivExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>parenthesisExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisExp([NotNull] SpreadsheetParser.ParenthesisExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>idAtomExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdAtomExp([NotNull] SpreadsheetParser.IdAtomExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>addSubExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddSubExp([NotNull] SpreadsheetParser.AddSubExpContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>functionExp</c>
	/// labeled alternative in <see cref="SpreadsheetParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionExp([NotNull] SpreadsheetParser.FunctionExpContext context);
}
} // namespace AntlrTutorial
