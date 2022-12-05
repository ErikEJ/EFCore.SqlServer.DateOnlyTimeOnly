ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly
========================================

TODO - add status labels

Adds DateOnly and TimeOnly support to the SQL Server EF Core provider.

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

TODO update sample

```cs
dbContext.AddRange(
    new EventSchedule { StartDate =  };
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

* [Date and Time Data Types (SQL Server)](https://learn.microsoft.com/sql/t-sql/functions/date-and-time-data-types-and-functions-transact-sq)
* [Entity Framework documentation](https://docs.microsoft.com/ef/)
