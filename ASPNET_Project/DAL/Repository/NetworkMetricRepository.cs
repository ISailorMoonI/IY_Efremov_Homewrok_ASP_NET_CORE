using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
   

    public class NetWorkMetricsRepository : INetworkMetricsRepository
    {
        public IList<NetworkMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnection.DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText =
                "SELECT * FROM networkmetrics WHERE (agent_id=@agentId) and ((time>=@fromTime) AND (time<=@toTime))";

            cmd.Parameters.AddWithValue("@agentId", agentId);
            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<NetworkMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetric
                    {
                        Id = reader.GetInt32(0),
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).ToUniversalTime()
                    });
                }
            }

            return returnList;
        }

        public IList<NetworkMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            using var connection = new SQLiteConnection(DataBaseConnection.DataBaseConnectionSettings.ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)";

            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Prepare();

            var returnList = new List<NetworkMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetric
                    {
                        Id = reader.GetInt32(0),
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).ToUniversalTime()
                    });
                }
            }

            return returnList;
        }
    }
}
