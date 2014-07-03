using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace AkismetModule
{
    [ObjectInfo("AkismetResources", ResourceClassId = "AkismetResources")]
    public class AkismetResources : Resource
    {
        public AkismetResources()
            : base()
        {

        }

        public AkismetResources(ResourceDataProvider dataProvider) : base(dataProvider)
        {

        }

        [ResourceEntry("AkismetSettingsTitle",
            Value = "Akismet Settings", 
            Description = "Settings for the Akismet module",
            LastModified = "2012/09/28")]
        public string AkismetSettingsTitle
        {
            get { return this["AkismetSettingsTitle"]; }
        }

        [ResourceEntry("AkismetBasicSettingsViewTitle",
            Value = "Akismet Settings",
            Description = "The title of the settings screen for Akismet",
            LastModified = "2012/10/1")]
        public string AkismetBasicSettingsViewTitle
        {
            get { return this["AkismetBasicSettingsViewTitle"]; }
        }

        [ResourceEntry("ProtectForumsCaption",
            Value = "Check for spam in Forums",
            Description = "The caption of the checkbox in the Akismet settings",
            LastModified = "2012/01/02")]
        public string ProtectForumsCaption
        {
            get { return this["ProtectForumsCaption"]; }
        }

        [ResourceEntry("ProtectCommentsCaption",
            Value = "Check for spam in Comments",
            Description = "The caption of the checkbox in the Akismet settings",
            LastModified = "2012/01/02")]
        public string ProtectCommentsCaption
        {
            get { return this["ProtectCommentsCaption"]; }
        }
    }
}
