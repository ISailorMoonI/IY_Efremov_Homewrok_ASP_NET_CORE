using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class DataBaseConnection
    {
        public class DataBaseConnectionSettings
        {
            public const string ConnectionString = "Data Source=metricsManager.db;Version=3;Pooling=true;Max Pool Size=100;";
        }
    }
}
