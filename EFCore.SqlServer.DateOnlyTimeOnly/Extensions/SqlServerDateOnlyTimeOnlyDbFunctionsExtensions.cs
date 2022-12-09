using System;

namespace Microsoft.EntityFrameworkCore
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class SqlServerDateOnlyTimeOnlyDbFunctionsExtensions
    {
        public static int DateDiffYear(this DbFunctions _, DateOnly startDate, DateOnly endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffYear(this DbFunctions _, DateOnly? startDate, DateOnly? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMonth(this DbFunctions _, DateOnly startDate, DateOnly endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMonth(this DbFunctions _, DateOnly? startDate, DateOnly? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffDay(this DbFunctions _, DateOnly startDate, DateOnly endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffDay(this DbFunctions _, DateOnly? startDate, DateOnly? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffWeek(this DbFunctions _, DateOnly startDate, DateOnly endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffWeek(this DbFunctions _, DateOnly? startDate, DateOnly? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
