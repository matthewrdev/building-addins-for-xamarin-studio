using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
	"BuildingXamarinStudioAddins", // The id for your addin
	Namespace = "BuildingXamarinStudioAddins", // The namespace of your addin
	Version = "1.0" // A semantic version number for your addin.
)]

/// The AddinName assembly attribute specifies the Name of your addin. 
/// The value that you provide here be the name that appears in the Addin Manager.
[assembly: AddinName("Translate .NET String")]

/// The AddinCategory assembly attribute specifies the addin category of your addin.
/// This is the cateogory name in the Addin Manager that your addin will appear under/
[assembly: AddinCategory("IDE extensions")]

/// The AddinDescription assembly attribute should clearly describe what your addin does.
/// This is the value that appears under the name in the right-hand pane inside the addin manager when a user has selected your addin.
[assembly: AddinDescription("This addin adds a right click menu item in a C# source code file that translates a given string literal to another langauge.\n\nThis is the accompanying addin to the Xamarin University guest lecture 'Building Xamarin Studio Addins' by Matthew Robbins.")]

/// The AddinAuthor assembly attribute states the author/owner of the addin
[assembly: AddinAuthor("matthewrobbins")]

/// The AddinUrl assembly attribute provides a link the user can click on to get more information about your addin.
/// This is the url that will open when the user clicks the "More Information" button in the Addin Manager.
[assembly: AddinUrl("https://github.com/matthewrdev/building-addins-for-xamarin-studio")]
