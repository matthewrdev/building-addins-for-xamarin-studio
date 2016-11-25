using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Addins;

namespace BuildingXamarinStudioAddins
{
	public static class ExtensionPointHelper
	{
		public static void RenderExtensionPointHierachy(string extensionPath)
		{
			// "/MonoDevelop/Ide/Commands"
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
