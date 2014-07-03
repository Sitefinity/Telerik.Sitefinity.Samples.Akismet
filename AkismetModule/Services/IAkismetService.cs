using System;
using System.Linq;
using System.ServiceModel;
using AkismetModule.Model;
using System.ServiceModel.Web;

namespace AkismetModule.Services
{
    [ServiceContract]
    public interface IAkismetService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AkismetViewModel GetSettings();

        [OperationContract]
        [WebInvoke(UriTemplate = "/", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void SaveSettings(AkismetViewModel viewModel);
    }
}
