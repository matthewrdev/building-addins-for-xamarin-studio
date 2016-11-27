using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace BuildingXamarinStudioAddins
{
	/// <summary>
	/// We can implement a startup routine by implementing a CommandHandler and then injecting it
	/// into the extension path /MonoDevelop/Ide/StartupHandlers".
	/// 
	/// This command handler will be executed:
	///  * When the user first installs the Addin.
	///  * Each time the IDE is started.
	/// </summary>
	public class StartupHandler : CommandHandler
	{
		public StartupHandler()
		{
		}

		/// <summary>
		/// In the Run override of this command handler we should do any first-run initialisation our addin may require.
		/// 
		/// Here we inspect the PropertyService for our api key property and then open the configuration dialog if it's
		/// missing.
		/// </summary>
		protected override void Run()
		{ 
			if (PropertyService.HasValue(PropertyKeys.TranslationApiPropertyKey) == false)
			{
				// Show the API key dialog.
				var dialog = new ConfigureApiKeyDialog ();
				dialog.Show ();
			}
		}
	}
}
