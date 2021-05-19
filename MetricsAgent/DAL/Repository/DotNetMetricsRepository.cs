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
    
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public IList<DotNetMetric> GetFromTimeToTime(long fromTime, long toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>(
                    "SELECT Id, Time, Value FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime,
                    }).ToList();
            }
        }

        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(DataBaseConnectionSettings.ConnectionString);
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUniversalTime().ToUnixTimeSeconds()
                    });
            }
        }
    }
}

