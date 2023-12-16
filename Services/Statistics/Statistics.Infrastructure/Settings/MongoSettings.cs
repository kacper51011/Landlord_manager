using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Infrastructure.Settings
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ApartmentsCollectionName { get; set; } = null!;
        public string RoomsCollectionName { get; set; } = null!;

        public string TenantsCollecyionName { get; set; } = null!;
    }
}
