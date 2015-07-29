using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace AkismetModule
{
    [ObjectInfo(Title = "Akismet module config title", Description = "Akismet module config description")]
    public class AkismetModuleConfig : ConfigSection
    {
        [ConfigurationProperty("apiKey", DefaultValue = "", IsRequired = true)]
        [ObjectInfo(Title = "Akismet API key that you can get from your Akismet account", Description = "Akismet API key that you can get from your Akismet account")]
        public string ApiKey
        {
            get
            {
                return (string)this["apiKey"];
            }

            set
            {
                this["apiKey"] = value;
            }
        }

        [ConfigurationProperty("protectComments", DefaultValue = true, IsRequired = true)]
        [ObjectInfo(Title = "Catch spam in comments", Description = "Enable the Akismet module to check new comments for spam")]
        public bool ProtectComments
        {
            get
            {
                return (bool)this["protectComments"];
            }

            set
            {
                this["protectComments"] = value;
            }
        }

        [ConfigurationProperty("protectForums", DefaultValue = true, IsRequired = true)]
        [ObjectInfo(Title = "Catch spam in forums", Description = "Enable the Akismet module to check new threads and posts for spam")]
        public bool ProtectForums 
        { 
            get
            {
                return (bool)this["protectForums"];
            }

            set 
            {
                this["protectForums"] = value;
            }
        }
    }
}
