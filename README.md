Telerik.Sitefinity.Samples.Akismet
==================================

[![Build Status](http://sdk-jenkins-ci.cloudapp.net/buildStatus/icon?job=Telerik.Sitefinity.Samples.Akismet.CI)](http://sdk-jenkins-ci.cloudapp.net/job/Telerik.Sitefinity.Samples.Akismet.CI/)

The Akismet sample project demonstrates how you can use the Akismet service to filter spam in forum posts and comments. The Akismet sample uses the Sitefinity event architecture. On the event of creating or updating an item, the Akismet service is called to verify whether the item is spam or not. 

In addition, the Akismet sample demonstrates how to call a third-party service from Sitefinity.


### Requirements

* Sitefinity license
 
* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Installation instructions: SDK Samples from GitHub


1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **StartupConfig.config** file. 
2. Modify the **dbType**, **sqlInstance** and **dbName** values to match your server settings.
3. Build the solution.

For version-specific details about the required Sitefinity NuGet packages for this sample application, click on [Releases]
 (https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Akismet/releases).
 
### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)
