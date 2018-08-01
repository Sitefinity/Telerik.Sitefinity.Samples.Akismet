Progress.Sitefinity.Samples.Akismet
==================================

### This repository is not automatically upgraded to latest Sitefintiy version. The repository is monitored for pull requests and fixes. The latest official version of Sitefinity that supports this sample is 9.1. Be aware that using a higher version could cause unexpected behavior. If you successfully upgrade the example to a greater version, please share your work with the community by submitting your changes via pull request.

The Akismet sample project demonstrates how you can use the Akismet service to filter spam in forum posts and comments. The Akismet sample uses the Sitefinity CMS event architecture. On the event of creating or updating an item, the Akismet service is called to verify whether the item is spam or not. 

In addition, the Akismet sample demonstrates how to call a third-party service from Sitefinity CMS.


### Requirements
* Sitefinity CMS license
* .NET Framework 4.5
* Visual Studio 2012
* Microsoft SQL Server 2008R2 or later versions

### Nuget package restoration
The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
For a full list of the referenced packages and their versions see the [packages.config](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Akismet/blob/master/SitefinityWebApp/packages.config) file.    
For a history and additional information related to package versions on different releases of this repository, see the [Releases page](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Akismet/releases).


### Installation instructions: SDK Samples from GitHub

1. In Solution Explorer, navigate to _SitefinityWebApp_ » *App_Data* » _Sitefinity_ » _Configuration_ and select the **StartupConfig.config** file. 
2. Modify the **dbType**, **sqlInstance** and **dbName** values to match your server settings.
3. Build the solution.

For version-specific details about the required Sitefinity NuGet packages for this sample application, click on [Releases] (https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Akismet/releases).
 
### Login

To login into the Sitefinity CMS backend, use the following credentials: 

**Username:** admin   
**Password:** password

### Additional resources
Progress Sitefinity CMS Documentation:   
[Development: Use and extend Sitefinity CMS functionality](http://docs.sitefinity.com/develop-create-and-manage-website-content)
