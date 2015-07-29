using System;
using System.Data.Entity;
using System.Linq;
using AkismetModule.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;

namespace AkismetModule
{
    public class AkismetEntityContext : DbContext
    {
        // call the base constructor with the default connection string from DataConfig.config
        // to use the same DB.
        public AkismetEntityContext()
            : base(Config.Get<DataConfig>().ConnectionStrings["Sitefinity"].ConnectionString)
        {
        }

        public DbSet<AkismetData> AkismetDataList { get; set; }
    }
}
