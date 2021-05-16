using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.DAL.Repository
{
    

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public IList<RamMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics").ToList();
            }
        }

        public IList<RamMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }
    }
}

