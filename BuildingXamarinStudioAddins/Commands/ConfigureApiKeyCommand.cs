using System;
using MonoDevelop.Components.Commands;

namespace BuildingXamarinStudioAddins
{
	/// <summary>
	/// The ConfigureApiKeyCommand opens our api key configuration dialog. 
	/// 
	/// This command is registered into the "/MonoDevelop/Ide/MainMenu/Tools" extension path;
	/// this causes a new menu item to be available within the "Tools" menu of the top action menu
	/// within the IDE.
	/// 
	/// As this dialog must always be accessible to our user, the Update method
	/// always enables the command for execution.
	/// 
	/// Our run method creates a new instance of our dialog and then opens it for our user.
	/// </summary>
	public class ConfigureApiKeyCommand : CommandHandler
	{
		public ConfigureApiKeyCommand()
		{
		}

		/// <summary>
		/// The Update method is used here to enable and make this command always visible.
		/// 
		/// We can use the Enabled property of CommandInfo to control if the command can be executed.
		/// We can use the Visible property of CommandInfo to control if the command is visible within menus.
		/// </summary>
		/// <param name="info">Info.</param>
		protected override void Update (CommandInfo info)
		{
			info.Enabled = true;
			info.Visible = true;
		}

		protected override void Run()
		{
			var dialog = new ConfigureApiKeyDialog ();
			dialog.Show ();
		}
	}
}
