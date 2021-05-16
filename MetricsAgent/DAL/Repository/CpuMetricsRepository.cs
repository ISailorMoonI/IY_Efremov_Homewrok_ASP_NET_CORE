using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Models;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.Repository
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }

        public IList<CpuMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }
    }
}





