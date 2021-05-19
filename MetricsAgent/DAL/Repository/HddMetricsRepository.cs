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
   

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

            public HddMetricsRepository()
            {
                SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
            }

            public IList<HddMetric> GetFromTimeToTime(long fromTime, long toTime)
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    return connection.Query<HddMetric>(
                        "SELECT Id, Time, Value FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                        new
                        {
                            fromTime = fromTime,
                            toTime = toTime,
                        }).ToList();
                }
            }

            public void Create(HddMetric item)
            {
                using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
                {
                    connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                        new
                        {
                            value = item.Value,
                            time = item.Time.ToUniversalTime().ToUnixTimeSeconds()
                        });
                }
            }
        }
}
