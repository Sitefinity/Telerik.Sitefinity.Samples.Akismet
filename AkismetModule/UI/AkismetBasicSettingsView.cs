using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.Web.UI.Fields;

namespace AkismetModule.UI
{
    public class AkismetBasicSettingsView : BasicSettingsView
    {
        protected override void InitializeViews()
        {
            base.InitializeViews();
        }

        #region Control References

        protected virtual ChoiceField ProtectCommentsField
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("protectComments", true);
            }
        }

        protected virtual ChoiceField ProtectForumsField
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("protectForums", true);
            }
        }

        protected virtual TextField ApiKeyField
        {
            get
            {
                return this.Container.GetControl<TextField>("akismetApiKey", true);
            }
        }

        #endregion

        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return AkismetBasicSettingsView.layoutTemplateName;
                return base.LayoutTemplatePath;
            }
        }

        private static string GenerateVirtualPath(string path)
        {
            var virtualPathConfig = Telerik.Sitefinity.Configuration.Config.Get<VirtualPathSettingsConfig>();
            VirtualPathElement element = null;
            virtualPathConfig.VirtualPaths.TryGetValue("~/SFAkismet/*", out element);
            if (element == null)
            {
                var jobsModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = "~/SFAkismet/*",
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = "AkismetModule"
                };
                virtualPathConfig.VirtualPaths.Add(jobsModuleVirtualPathConfig);
                ConfigManager.GetManager().SaveSection(virtualPathConfig);
                VirtualPathManager.Reset();
            }
            return "~/SFAkismet/" + path;
        }

        #region IScriptControl members

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var baseRef = new List<ScriptReference>(base.GetScriptReferences());
            var akismetBasicSettingsViewScript = new ScriptReference(AkismetBasicSettingsView.script, typeof(AkismetBasicSettingsView).Assembly.FullName);
            baseRef.Add(akismetBasicSettingsViewScript);
            return baseRef;
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var baseDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var akismetSD = new ScriptControlDescriptor(typeof(AkismetBasicSettingsView).FullName, this.ClientID);

            akismetSD.AddComponentProperty("protectCommentsField", this.ProtectCommentsField.ClientID);
            akismetSD.AddComponentProperty("protectForumsField", this.ProtectForumsField.ClientID);
            akismetSD.AddComponentProperty("apiKeyField", this.ApiKeyField.ClientID);
            akismetSD.AddComponentProperty("messageControl", this.Message.ClientID);
            akismetSD.AddElementProperty("saveButton", this.SaveButton.ClientID);

            baseDescriptors.Add(akismetSD);
            return baseDescriptors;
        }

        #endregion

        public static readonly string layoutTemplateName = GenerateVirtualPath("AkismetModule.UI.AkismetBasicSettingsView.ascx");
        private static readonly string script = "AkismetModule.UI.AkismetBasicSettingsView.js";
    }
}
