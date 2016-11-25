using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace BuildingXamarinStudioAddins
{
	public class StartupHandler : CommandHandler
	{
		public StartupHandler()
		{
		}

		protected override void Run()
		{
			if (PropertyService.HasValue(PropertyKeys.TranslationApiPropertyKey) == false)
			{
				// Show the API key dialog.

			}
		}
	}
}
