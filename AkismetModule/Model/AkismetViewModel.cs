using System;
using System.Linq;

namespace AkismetModule.Model
{
    public class AkismetViewModel
    {
        public string ApiKey { get; set; }

        public bool ProtectComments { get; set; }

        public bool ProtectForums { get; set; }
    }
}
