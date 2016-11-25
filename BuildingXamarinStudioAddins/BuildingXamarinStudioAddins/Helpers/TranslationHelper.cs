using System;
using System.Net;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Dialogs;

namespace BuildingXamarinStudioAddins
{
	public static class TranslationHelper
	{
		public const string TranslationEndpoint = @"https://translation.googleapis.com/language/translate/v2?";

		public static string Translate(string source, string targetLanguage)
		{
			ProgressDialog progressDialog = new ProgressDialog(IdeApp.Workbench.RootWindow, false, true);
			progressDialog.Show();

			string translationResult = "";
			try
			{
				string apiKeyParam = $"key={PropertyService.Get(PropertyKeys.TranslationApiPropertyKey, "")}";
				string languageParam = $"";
				string sourceTextParam = $"";


					var url = TranslationEndpoint + string.Join("&", apiKeyParam, languageParam, sourceTextParam);

					var client = new WebClient();
					client.DownloadProgressChanged += (sender, e) =>
					{
						Runtime.RunInMainThread(() =>
						{
							progressDialog.Progress = (double)e.ProgressPercentage / 100.0; ;
						});
					};

					var content = new WebClient().DownloadString(url);

			}
			catch (Exception ex)
			{
				if (progressDialog != null)
				{
					progressDialog.Hide();
				}
			}


			return translationResult;
		}
	}
}
