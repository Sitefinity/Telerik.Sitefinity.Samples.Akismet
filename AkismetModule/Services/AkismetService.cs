using System;
using System.Linq;
using AkismetModule.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Services;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace AkismetModule.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AkismetService : IAkismetService
    {
        public AkismetViewModel GetSettings()
        {
            ServiceUtility.RequestAuthentication();
            var config = Config.Get<AkismetModuleConfig>();

            return new AkismetViewModel()
            {
                ApiKey = config.ApiKey,
                ProtectComments = config.ProtectComments,
                ProtectForums = config.ProtectForums
            };
        }

        public void SaveSettings(AkismetViewModel viewModel)
        {
            ServiceUtility.RequestAuthentication();
            var configManager = ConfigManager.GetManager();
            var section = configManager.GetSection<AkismetModuleConfig>();

            section.ApiKey = viewModel.ApiKey;
            section.ProtectForums = viewModel.ProtectForums;
            section.ProtectComments = viewModel.ProtectComments;

            configManager.SaveSection(section);
        }
    }
}
