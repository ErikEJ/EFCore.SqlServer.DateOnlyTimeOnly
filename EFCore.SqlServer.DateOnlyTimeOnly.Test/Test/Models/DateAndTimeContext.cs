using Microsoft.EntityFrameworkCore.SqlServer.Test.Logging;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Test.Models
{
    class DateAndTimeContext : DbContext
    {
        readonly TestLoggerFactory _loggerFactory
            = new TestLoggerFactory();

        public DbSet<EventSchedule> Events { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HierarchyIdTests",
                    x => x.UseDateOnlyTimeOnly())
                .UseLoggerFactory(_loggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSchedule>()
                .HasData(
                    new EventSchedule { Id = 1, StartDate = new DateOnly(2022, 12, 13), StartTime = new TimeOnly(9, 9) },
                    new EventSchedule { Id = 2, StartDate = new DateOnly(2022, 12, 24), StartTime = new TimeOnly(10, 10) },
                    new EventSchedule { Id = 3, StartDate = new DateOnly(1758, 12, 24), StartTime = new TimeOnly(11, 11) });
        }
    }
}
