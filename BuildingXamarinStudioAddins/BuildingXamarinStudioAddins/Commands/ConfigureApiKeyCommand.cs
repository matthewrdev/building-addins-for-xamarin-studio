using System;
using MonoDevelop.Components.Commands;

namespace BuildingXamarinStudioAddins
{
	public class ConfigureApiKeyCommand : CommandHandler
	{
		public ConfigureApiKeyCommand()
		{
		}

		protected override void Update (CommandInfo info)
		{
			info.Enabled = true;
			info.Visible = true;
			base.Update (info);
		}

		protected override void Run()
		{
			var dialog = new ConfigureApiKeyDialog ();
			dialog.Show ();
			
			base.Run();
		}
	}
}
