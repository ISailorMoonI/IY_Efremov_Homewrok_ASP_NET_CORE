using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace MetricsAgent.DAL.Migrations
{
    [Migration(4)]
    public class NetworkMigration : Migration
    {
        public override void Up()
        {
            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
           
        }
    }
}
