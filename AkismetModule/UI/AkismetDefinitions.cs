using System;
using System.Linq;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity;
using System.Collections.Generic;

namespace AkismetModule.UI
{
    public class AkismetDefinitions
    {
        #region Constants

        public static string BackendDefinitionName = "AkismetBackend";
        public static string BackendListViewName = "AkismetBackendList";

        #endregion

        //internal static ContentViewControlElement DefineBackendContentView(ConfigElement parent)
        //{
        //    var fluentContentView = App.WorkWith().Module().DefineContainer(parent, AkismetDefinitions.BackendDefinitionName, typeof(AkismetModuleConfig));
        //    var backendConentView = fluentContentView.Get();

        //    var externalScripts = new Dictionary<string, string>();
        //    externalScripts.Add("AkismetModule.UI.AkismetBasicSettingsView.js, AkismetModule", "OnMasterViewLoaded");

        //    var fluentMasterView =
        //        fluentContentView
        //            .AddMasterView(BackendListViewName)
        //                .DisablePaging()
        //                .SetServiceBaseUrl("~/Sitefinity/Services/Akismet/AkismetSettingsService.svc/")
        //                .SetExternalClientScripts(externalScripts);
        //    var appsGridView = fluentMasterView.Get();


        //}
    }
}
