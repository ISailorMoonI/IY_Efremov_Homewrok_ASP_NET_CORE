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
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public IList<NetworkMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString))
            {
                return connection.Query<NetworkMetric>(
                    "SELECT Id, Time, Value FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUniversalTime().ToUnixTimeSeconds()
                    });
            }
        }
    }
}
