using Microsoft.EntityFrameworkCore.SqlServer.Test.Logging;

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
            //modelBuilder.Entity<Patriarch>()
            //    .HasData(
            //        new Patriarch { Id = HierarchyId.GetRoot(), Name = "Abraham" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/"), Name = "Isaac" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/"), Name = "Jacob" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/1/"), Name = "Reuben" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/2/"), Name = "Simeon" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/3/"), Name = "Levi" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/4/"), Name = "Judah" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/5/"), Name = "Issachar" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/6/"), Name = "Zebulun" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/7/"), Name = "Dan" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/8/"), Name = "Naphtali" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/9/"), Name = "Gad" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/10/"), Name = "Asher" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/11.1/"), Name = "Ephraim" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/11.2/"), Name = "Manasseh" },
            //        new Patriarch { Id = HierarchyId.Parse("/1/1/12/"), Name = "Benjamin" });
        }
    }
}
