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


1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.
3. Build the solution.


### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)
