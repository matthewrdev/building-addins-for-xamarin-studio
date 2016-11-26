using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
	"BuildingXamarinStudioAddins",
	Namespace = "BuildingXamarinStudioAddins",
	Version = "1.0"
)]

/// The AddinName assembly attribute specifies the Name of your addin. 
/// The value that you provide here be the name that appears in the Addin Manager.
[assembly: AddinName("Translate .NET String")]

/// The AddinCategory assembly attribute specifies the addin category of your addin.
/// 
[assembly: AddinCategory("IDE extensions")]

/// The AddinDescriptio
[assembly: AddinDescription("This addin adds a right click menu item in a C# source code file that translates a given string literal to another langauge.\n\nThis is the accompanying addin to the Xamarin University guest lecture 'Building Xamarin Studio Addins' by Matthew Robbins.")]

[assembly: AddinAuthor("matthewrobbins")]

[assembly: AddinUrl("https://github.com/matthewrdev/building-addins-for-xamarin-studio")]
