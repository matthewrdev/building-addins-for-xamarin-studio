using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Addins;

namespace BuildingXamarinStudioAddins
{
	public static class ExtensionPointHelper
	{
		/// <summary>
		/// Given an extension point path such as "/MonoDevelop/Ide/Commands", this helper method retrieves
		/// all nodes within that extension and then renders the ids that can be referenced within the 
		/// Manifest.addin.xml document.
		/// 
		/// This helper is extremely useful when diagnosing why an addin's command isn't functioning as expected.
		/// Frequently another command consumes an event BEFORE your addin; this method let's you insepect an 
		/// extension path and find the id's of the nodes so you can inject your command before it using the 
		/// insertbefore attribute.
		/// </summary>
		/// <param name="extensionPath">The extension path to render.</param>
		public static void RenderExtensionPointHierachy(string extensionPath)
		{
			var nodes = AddinManager.AddinEngine.GetExtensionNodes(extensionPath).GetEnumerator();

			var objs = new List<TypeExtensionNode>();
			while (nodes.MoveNext())
			{
				objs.Add(nodes.Current as TypeExtensionNode);
			}

			var names = objs.Where(t => t != null).Select(t => t.Id + " | " + t.TypeName).ToList();

			foreach (var n in names)
			{
				Console.WriteLine(n);
			}
		}
	}
}
