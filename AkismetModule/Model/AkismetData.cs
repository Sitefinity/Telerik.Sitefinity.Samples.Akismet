using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AkismetModule.Model
{
    public class AkismetData
    {
        [Key]
        public Guid AkismetDataId { get; set; }

        public string UserIp { get; set; }

        public string Referrer { get; set; }

        public string UserAgent { get; set; }

        public string ItemType { get; set; }

        public Guid ContentItemId { get; set; }
    }
}
