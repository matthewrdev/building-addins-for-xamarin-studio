# Building Xamarin Studio Addins
**Matthew Robbins - Creator Of [MFractor](http://www.mfractor.com/)**

This repository is the accompanying source code for the Xamarin University guest lecture, **Building Xamarin Studio Addins For Xamarin Studio**.

Learn how to take an Addin from concept to deployment using the Addin Maker, Xamarin Studio and mdtool.

This source code contains a **Translate String** Addin; users can right click on a C# string literal and translate it to another language!

![translation demo](images/translate-demo.gif)

### Table of Contents

  * Installing the Addin Maker
  * Creating Your First Addin
  * The MonoDevelop Extension Model
  * The Addin Manifest
  * Addin References
  * Command Handlers
  * Some Essential APIs
  * Testing and Debugging
  * Packaging Your Addin
  * Publishing Your Addin
  * Resources
  * Closing Words

## Installing the Addin Maker
Let's get started by installing the Addin Maker into Xamarin Studio.

The Addin Maker is an [open source](https://github.com/mhutch/MonoDevelop.AddinMaker), freely available addin developed by [Mikayla Hutchinson](https://twitter.com/mjhutchinson) that enables development and debugging of Xamarin Studio addins *within* Xamarin Studio itself.

We can install the Addin Maker through Xamarin Studios *Addin Manager*; click on the **Xamarin Studio** main menu then **Addins..** to open the Addin Manager.

Browse to **Gallery** and then select the **Addin Development**.

![Add repository reference](images/addin-maker.gif)

If the Addin Manager's Gallery is empty (this can happen when you upgrade then downgrade Xamarin Studio), then manually add a reference to the MonoDevelop addin repository using the following URL:

[http://addins.monodevelop.com/Stable/Mac/6.1.1/main.mrep](http://addins.monodevelop.com/Stable/Mac/6.1.1/main.mrep)

![Add repository reference](images/add-addin-repo.gif)

## Creating Your First Addin
After installing the Addin Maker, a new project type will be available within the project creation dialog.

Select the **File** main menu item and then **New Solution**. Under **Miscellaneous** an option named **Xamarin Studio Addin** will now be present.

![Xamarin Studio Addin Menu Item](images/new-addin-project.png)

Go ahead and create your first addin!

## The MonoDevelop Extension Model
Before we get coding, it's very important to cover some theory.

MonoDevelop is built upon an extensible architecture known as an *Extension Model*. This architecture




## The Addin Manifest

You'll see we now have a blank project with a few files under the projects **Properties** folder:

![The Addin project structure](images/addin-project-structure.png)



## Project Addin References

## Command Handlers
One of the most common objects we'll create when building a Xamarin Studio addin are implementations of `CommandHandler`'s.

A `CommandHandler` is an action that can be executed within a certain context within Xamarin Studio; we register a

## Some Essential APIS
Let's dig through some of the essential APIs that you may need to use:

#### IdeApp
The `MonoDevelop.Ide.IdeApp` static class is your entry point into most of Xamarin Studio. It exposes the `Workbench`, the `Workspace`, various services and life cycle methods such as when the IDE is exiting.

Typically the `IdeApp` class is used to access the `Workbench` and `Workspace`.

#### IdeApp.Workbench
The `Workbench` is used to access the documents that a developer is currently working with. We can load, create, reparse and close documents, access editor pads, access the root ide window and much much more.

One of the most commonly used properties is `ActiveDocument`. This property retrieves the current document that a user is working with. From there, we can grab the C# syntax tree, a users project or solution and then analyse or make changes to their source code.

#### IdeApp.Workspace
The `Workspace` is used to access the general state of the Ide during a user session.

We can open new projects and documents, detect file open, close and editing events and much, much more.

The workspace is most commonly used to detect changes to a users project and documents and then update a state within our addins.

#### RefactoringService
When developing an Addin, we'll often want to make changes to a users source code and have undo-redo history recorded.

The `RefactoringService` allows us to apply changes to a users document and have undo-redo history automatically recorded.

When applying a change, we provide the `RefactoringService` instances of the `MonoDevelop.Refactoring.Change` object that the service should apply.

Some common changes we can perform:
 * `TextReplaceChange`: Replaces a section of text within a document with new content.
 * `SaveProjectChange`: Saves a targetted project.
 * `CreateFileChange`: Creates a new file with the provided content.

 The `Change` object is abstract so we can implement our own custom refactoring changes if we'd like.

#### PropertyService
The static class `MonoDevelop.Core.PropertyService` is used to store user configurations for the Ide installation.

The `PropertyService` is commonly used to store configuration settings that are specific to your addin. For instance, we can store a variable that denotes if it was the addins first run using the code below:

````
if (!PropertyService.HasValue("my_addin.is_first_run")
   || PropertyService.Get("my_addin.is_first_run", true))
{
  PropertyService.Set("my_addin.is_first_run", false);
  PropertyService.SaveProperties();
}
````

Here we:
 * Use `HasValue` to determine if a value is stored within the users properties.
 * Use `Get` to retrive a value from the `PropertyService`; note that we can provide a default value to return when there property does exist.
 * Use `Set` to store a new property.
 * Use `SaveProperties` to commit our changes to the properties. You **must** do this to ensure your properties are persisted between Ide sessions.

## Testing And Debugging


## Packaging Your Addin
Before we send out our addin into the big, wide world it's best practice to verify everything works as a final app package.

Xamarin Studio addins are distributed in the `.mpack` format; this is a zip archive that contains all your Addin assemblies and resources in a convenient bundle.

We use `mdtool` to accomplish this. On a typical OSX installation, `mdtool` will be location at `/Applications/Xamarin Studio.app/Contents/MacOS/mdtool`.

If `mdtool` is not present at that location, you can find it using the following shell script:

```
find / -name mdtool
```

To package an addin, we use provide the path to our addin assembly to mdtools `setup pack` command along with an output directory using the `-d:` flag like so:

```
/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool setup pack ./MyAddin/bin/Release/MyAddin.dll -d:./builds/mpack/$BUILD_DATE
```

This command will generate a `*.mpack` file that bundles your addin. You can then install this addin via the **Addin Manager** using **Install from file**:

![install from mpack](images/install-mpack.png)

For a convenient packaging script, see [package_mpack.sh](package_mpack.sh)

## Publishing Your Addin
We are almost there!

The last thing we want to do is to include our Addin into the MonoDevelop Addin Repository; this online repository will build, package and host our addin and then make it available within the **Addin Manager** within Xamarin Studio.

Firstly, we need to provide an **addin-project.xml** file that tells the addin repository how to build and then package our addin:

```
<AddinProject appVersion="6.0">
    <Project platforms="Mac Win32">
        <AddinFile>BuildingXamarinStudioAddins/bin/Release/BuildingXamarinStudioAddins.dll</AddinFile>
        <BuildFile>BuildingXamarinStudioAddins.sln</BuildFile>
        <BuildConfiguration>Release</BuildConfiguration>
    </Project>
</AddinProject>
```

Let's go through this line by line:
 * **AddinProject**: This must be the root element and it specifies the app version our addin targets at a minimum. 6.0 means that our addin **will not** be installable on versions of Xamarin Studio lower than 6.0.
 * **Project**: Specify the platforms we are building for.
 * **AddinFile**: Informs the build server where the final assembly it should package will be found relative to the root of our repository.
 * **BuildFile**: The file that the addin server should build.
 * **BuildConfiguration**: The configuration that the addin should be built under.

 After we've added this, visit the Addin Repository website and create an account. In the right side of the site click **Create a new project**.

 Here we configure the build server and tell where our addin's source code lives, the version control type, where the build configuration may be found and how to handle releases:

 ![addin build configuration](images/addin-repo-config.png)

## Resources

## Closing Words
