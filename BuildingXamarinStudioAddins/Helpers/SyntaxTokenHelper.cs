using System;
using System.Threading;
using MonoDevelop.Ide.Gui;

namespace BuildingXamarinStudioAddins
{
	public static class SyntaxTokenHelper
	{
		/// <summary>
		/// Inspects the C# abstract syntax tree for the provided document and finds the string SyntaxToken
		/// for the provided document location/
		/// </summary>
		/// <returns><c>true</c>, if syntax token was resolved, <c>false</c> otherwise.</returns>
		/// <param name="document">The document to inspect.</param>
		/// <param name="documentLocation">The document location in question.</param>
		/// <param name="token">The string SyntaxToken that was resolved</param>
		public static bool ResolveSyntaxToken(Document document, MonoDevelop.Ide.Editor.DocumentLocation documentLocation, out Microsoft.CodeAnalysis.SyntaxToken token)
		{
			var offset = document.Editor.LocationToOffset(documentLocation);
			return ResolveSyntaxToken(document, offset, out token);
		}

		/// <summary>
		/// Inspects the C# abstract syntax tree for the provided document and finds the string SyntaxToken
		/// for the provided document offset.
		/// </summary>
		/// <returns><c>true</c>, if syntax token was resolved, <c>false</c> otherwise.</returns>
		/// <param name="document">The document to inspect.</param>
		/// <param name="offset">The document offset in question.</param>
		/// <param name="syntaxToken">The string SyntaxToken that was resolved</param>
		public static bool ResolveSyntaxToken(Document document, int offset, out Microsoft.CodeAnalysis.SyntaxToken syntaxToken)
		{
			syntaxToken = new Microsoft.CodeAnalysis.SyntaxToken();
			if (document.AnalysisDocument == null)
			{
				return false;
			}

			if (document.Project == null)
			{
				return false;
			}

			var analysisDoc = document.AnalysisDocument;

			var token = new CancellationToken();
			var syntaxRoot = analysisDoc.GetSyntaxRootAsync(token).Result;

			var model = analysisDoc.GetSemanticModelAsync().Result;

			Microsoft.CodeAnalysis.SyntaxToken csharpToken = syntaxRoot.FindToken(offset);

			if (csharpToken.RawKind != (int)Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralToken)
			{
				return false;
			}

			syntaxToken = csharpToken;
			return true;
		}
	}
}
