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

		public static string Translate(string textToTranslate, string targetLanguage)
		{
			string translationResult = "";
			try
			{
				string apiKeyParam = $"key={PropertyService.Get(PropertyKeys.TranslationApiPropertyKey, "")}";
				string languageParam = $"target={targetLanguage}";
				string sourceTextParam = $"q={System.Web.HttpUtility.UrlEncode(textToTranslate)}";

				var url = TranslationEndpoint + string.Join("&", apiKeyParam, languageParam, sourceTextParam);

				var client = new WebClient();

				var content = client.DownloadString(url);

				dynamic translation = Newtonsoft.Json.JsonConvert.DeserializeObject (content);

				translationResult = translation.data.translations [0].translatedText;
			}
			catch (Exception ex)
			{
			}


			return translationResult;
		}
	}
}
