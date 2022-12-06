ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly
========================================

![build status](https://img.shields.io/github/workflow/status/ErikEJ/EFCore.SqlServer.DateOnlyTimeOnly/.NET/main) [![latest version](https://img.shields.io/nuget/v/ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly)](https://www.nuget.org/packages/ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly) [![downloads](https://img.shields.io/nuget/dt/ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly)](https://www.nuget.org/packages/ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly) ![license](https://img.shields.io/github/license/ErikEJ/EFCore.SqlServer.DateOnlyTimeOnly)

Adds .NET 6 or later `DateOnly` and `TimeOnly` support to the SQL Server EF Core 6 provider. These types map directly to the SQL Server `date` and `time` data types.

Installation
------------

The latest version is available on [NuGet](https://www.nuget.org/packages/ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly).

```sh
dotnet add package ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly
```

Compatibility
-------------

The following table show which version of this library to use with which version of EF Core.

| EF Core | Version to use  |
| ------- | --------------- |
| 6.0     | 6.0.x           |
| 7.0     | 7.0.x           |

Usage
-----

Enable DateOnly and TimeOnly support by calling UseDateOnlyTimeOnly inside UseSqlServer. UseSqlServer is is typically called inside `Startup.ConfigureServices` or `OnConfiguring` of your DbContext type.

```cs
options.UseSqlServer(
    connectionString,
    x => x.UseDateOnlyTimeOnly());
```

Add `DateOnly` and `TimeOnly` properties to your entity types.

```cs
class EventSchedule
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly TimeOfDay { get; set; }
}
```

Insert data.

```cs
dbContext.AddRange(
    new EventSchedule { StartDate = new DateOnly(2022, 12, 24), TimeOfDay = new TimeOnly(12, 00) };
dbContext.SaveChanges();
```

Query.

```cs
var eventsOfTheDay = from e in dbContext.EventSchedules
                      where e.StartDate == new DateOnly(2022, 10, 12)
                      select e;
```

See also
--------

* [Date Data Type (SQL Server)](https://learn.microsoft.com/sql/t-sql/data-types/date-transact-sql)
* [Time Data Type (SQL Server)](https://learn.microsoft.com/sql/t-sql/data-types/time-transact-sql)
* [Entity Framework documentation](https://learn.microsoft.com/ef/)
