Telerik.Sitefinity.Samples.Akismet
==================================

The Akismet sample project demonstrates how you can use the Akismet service to filter spam in forum posts and comments. The Akismet sample uses the Sitefinity event architecture. On the event of creating or updating an item, the Akismet service is called to verify whether the item is spam or not. 

In addition, the Akismet sample demonstrates how to call a third-party service from Sitefinity.


### Requirements

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Installation instructions: SDK Samples from GitHub


1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.
3. Build the solution.

The project refers to the following NuGet packages:

**AkismetModule** library

*	Telerik.Sitefinity.Core.nupkg

*	Telerik.Sitefinity.Content.nupkg

**SitefinityWebApp** library

*	Telerik.Sitefinity.All.nupkg

**Telerik.Sitefinity.Samples.Common** library

*	Telerik.Sitefinity.Core.nupkg

*	Telerik.Sitefinity.Content.nupkg

*	OpenAccess.Core.nupkg

You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).


### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)
