using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer.Test.Models;
using Xunit;

namespace Microsoft.EntityFrameworkCore.SqlServer
{
    public class QueryTests : IDisposable
    {
        private readonly DateAndTimeContext _db;

        public QueryTests()
        {
            _db = new DateAndTimeContext();
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
        }

        [Fact]
        public void GetDateOnly_can_translate()
        {
            var results = Enumerable.ToList(
                from p in _db.Events
                where p.StartDate == new DateOnly(2022, 12, 13)
                select p.Id);

            Assert.Equal(
                condense(@"SELECT [e].[Id] FROM [Events] AS [e] WHERE [e].[StartDate] = '2022-12-13'"),
                condense(_db.Sql));

            Assert.Equal(new[] { 1 }, results);
        }

        [Fact]
        public void GetDateOnly_can_translate_Parameter()
        {
            var startDate = new DateOnly(2022, 12, 13);

            var results = Enumerable.ToList(
                from p in _db.Events
                where p.StartDate == startDate
                select p.Id);

            Assert.Equal(
                condense(@"SELECT [e].[Id] FROM [Events] AS [e] WHERE [e].[StartDate] = @__startDate_0"),
                condense(_db.Sql));

            Assert.Equal(new[] { 1 }, results);
        }

        [Fact]
        public void GetTimeOnly_can_translate()
        {
            var results = Enumerable.ToList(
                from p in _db.Events
                where p.StartTime == new TimeOnly(9, 9)
                select p.Id);

            Assert.Equal(
                condense(@"SELECT [e].[Id] FROM [Events] AS [e] WHERE [e].[StartTime] = '09:09:00'"),
                condense(_db.Sql));

            Assert.Equal(new[] { 1 }, results);
        }

        [Fact]
        public void GetTimeOnly_can_translate_Parameter()
        {
            var startTime = new TimeOnly(9, 9);

            var results = Enumerable.ToList(
                from p in _db.Events
                where p.StartTime == startTime
                select p.Id);

            Assert.Equal(
                condense(@"SELECT [e].[Id] FROM [Events] AS [e] WHERE [e].[StartTime] = @__startTime_0"),
                condense(_db.Sql));

            Assert.Equal(new[] { 1 }, results);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        // replace whitespace with a single space
        private static string condense(string str)
        {
            var split = str.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", split);
        }
    }
}
