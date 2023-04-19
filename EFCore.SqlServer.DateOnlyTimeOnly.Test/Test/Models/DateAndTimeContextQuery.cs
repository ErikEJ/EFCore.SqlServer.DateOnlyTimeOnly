using Microsoft.EntityFrameworkCore.SqlServer.Test.Logging;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Test.Models
{
    class DateAndTimeContextQuery : DbContext
    {
        readonly TestLoggerFactory _loggerFactory
            = new TestLoggerFactory();

        public DbSet<EventScheduleQuery> Events { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DateOnlyTimeOnlyTests",
                    x => x.UseDateOnlyTimeOnly())
                .UseLoggerFactory(_loggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventScheduleQuery>()
                .HasData(
                    new EventScheduleQuery { Id = 1, StartDate = new DateOnly(2022, 12, 13), StartTime = new TimeOnly(9, 9, 9, 9) },
                    new EventScheduleQuery { Id = 2, StartDate = new DateOnly(2022, 12, 24), StartTime = new TimeOnly(10, 10, 10), LegacyDateTime = new DateTime(2022, 11, 24) },
                    new EventScheduleQuery { Id = 3, StartDate = new DateOnly(1758, 1, 1), StartTime = new TimeOnly(11, 11) });
        }
    }
}
