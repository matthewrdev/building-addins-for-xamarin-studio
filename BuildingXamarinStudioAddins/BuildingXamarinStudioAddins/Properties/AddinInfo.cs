using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
	"BuildingXamarinStudioAddins",
	Namespace = "BuildingXamarinStudioAddins",
	Version = "1.0"
)]

[assembly: AddinName("Translate .NET String")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("This addin adds a right click menu item in a C# source code file that translates a given string literal to another langauge.\n\nThis is the accompanying addin to the Xamarin University guest lecture 'Building Xamarin Studio Addins' by Matthew Robbins.")]
[assembly: AddinAuthor("matthewrobbins")]
