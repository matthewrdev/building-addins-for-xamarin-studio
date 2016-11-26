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
			
			base.Update(info);
		}

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

				TextReplaceChange translateChange = new TextReplaceChange ();
				translateChange.RemovedChars = token.Text.Length;
				translateChange.InsertedText = "\"" + translation + "\"";
				translateChange.Description = "Translate the existin string content to " + language;
				translateChange.Offset = token.SpanStart;
				translateChange.FileName = doc.FileName;

				var monitor = IdeApp.Workbench.ProgressMonitors.GetBackgroundProgressMonitor ("Apply translation", null);
				RefactoringService.AcceptChanges (monitor, new List<Change> () { translateChange });
			}


			base.Run(dataItem);
		}
	}
}
