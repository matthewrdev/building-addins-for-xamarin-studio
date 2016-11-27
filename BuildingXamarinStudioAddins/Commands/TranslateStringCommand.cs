using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Refactoring;

namespace BuildingXamarinStudioAddins
{
	/// <summary>
	/// The TranslateStringCommand takes a C# string literal beneath the users cursor and translates it
	/// to another language using Google Translation Services.
	/// 
	/// This command demonstrates multiple concepts:
	///  * Using Update to reject if the command cannot run. (User has no API key provided, cursor not over a string literal)
	///  * Using Update to add several sub-commands into the users context menu.
	///  * Passing a data object between Update and Run.
	///  * Implementing the Run callback to perform an action (translate a string literal).
	///  * Using the RefactoringService to apply a refactoring action to the users workspace.
	/// </summary>
	public class TranslateStringCommand : CommandHandler
	{
		public TranslateStringCommand()
		{
		}

		public class TranslationSetting
		{
			public string TargetLanguage;
			public SyntaxToken Token;

			public TranslationSetting(string language, SyntaxToken token)
			{
				TargetLanguage = language;
				Token = token;
			}
		}

		/// <summary>
		/// The Update method is a 
		/// </summary>
		/// <param name="info">Info.</param>
		protected override void Update(CommandArrayInfo info)
		{
			if (PropertyService.HasValue(PropertyKeys.TranslationApiPropertyKey) == false) {
				return; // Can't run 
			}

			var doc = IdeApp.Workbench.ActiveDocument;

			var editor = doc.Editor;

			if (doc != null
				&& editor != null) {
				
				var loc = editor.CaretLocation;
				Microsoft.CodeAnalysis.SyntaxToken token;
				if (SyntaxTokenHelper.ResolveSyntaxToken (doc, loc, out token)) {

					var commandSet = new CommandInfoSet ();

					commandSet.CommandInfos.Add (new CommandInfo ("English"), new TranslationSetting ("en", token));
					commandSet.CommandInfos.Add (new CommandInfo ("German"), new TranslationSetting("de", token));
					commandSet.CommandInfos.Add (new CommandInfo ("Spanish"), new TranslationSetting ("es", token));
					commandSet.CommandInfos.Add (new CommandInfo ("French"), new TranslationSetting ("fr", token));

					info.Add (commandSet);
				}
			}
		}

		/// <summary>
		/// Our Run override is used to perform an action.
		/// 
		/// Here we take the previously selected translation menu item and generate a translation for the string literal
		/// that is under the users cursor.
		/// 
		/// We then create a TextReplaceChange and apply it using the RefactoringService. This will write new text (source code)
		/// at the location that we specify and add an undo operation into the undo buffer for us.
		/// </summary>
		/// <param name="dataItem">Data item.</param>
		protected override void Run(object dataItem)
		{
			var doc = IdeApp.Workbench.ActiveDocument;

			var editor = doc.Editor;

			if (doc != null
				&& editor != null) {
				var loc = editor.CaretLocation;

				var setting = dataItem as TranslationSetting;
				var language = setting.TargetLanguage;
				var token = setting.Token;

				var text = token.Text.Substring (1, token.Text.Length - 2); // Grab the text without the "" characters.
				var translation = TranslationHelper.Translate (text, language);

				if (!string.IsNullOrEmpty (translation)) {

					// 
					TextReplaceChange translateChange = new TextReplaceChange ();
					translateChange.RemovedChars = token.Text.Length;
					translateChange.InsertedText = "\"" + translation + "\"";
					translateChange.Description = "Translate the existin string content to " + language;
					translateChange.Offset = token.SpanStart;
					translateChange.FileName = doc.FileName;

					var monitor = IdeApp.Workbench.ProgressMonitors.GetBackgroundProgressMonitor ("Apply translation", null);

					// Here we apply 
					RefactoringService.AcceptChanges (monitor, new List<Change> () { translateChange });
				}
			}
		}
	}
}
