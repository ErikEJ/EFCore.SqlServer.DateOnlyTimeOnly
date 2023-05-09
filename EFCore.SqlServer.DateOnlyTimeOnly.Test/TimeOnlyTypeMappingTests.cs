using System;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Microsoft.EntityFrameworkCore.SqlServer
{
    public class TimeOnlyTypeMappingTests
    {
        [Fact]
        public void Maps_int_column()
        {
            var mapping = CreateMapper().FindMapping(
                new RelationalTypeMappingInfo(
                    storeTypeName: "int",
                    storeTypeNameBase: "int",
                    unicode: null,
                    size: null,
                    precision: null,
                    scale: null));

            Assert.Null(mapping);
        }

        [Fact]
        public void Maps_timeonly_column()
        {
            var mapping = CreateMapper().FindMapping(
                new RelationalTypeMappingInfo(
                    storeTypeName: SqlServerTimeOnlyTypeMappingSourcePlugin.SqlServerTypeName,
                    storeTypeNameBase: SqlServerTimeOnlyTypeMappingSourcePlugin.SqlServerTypeName,
                    unicode: null,
                    size: null,
                    precision: null,
                    scale: null));

            AssertMapping<TimeOnly>(mapping);

            Assert.Equal("'15:31:00'", mapping.GenerateSqlLiteral(new TimeOnly(15, 31)));

            Assert.Equal("'15:31:00'", mapping.GenerateSqlLiteral(new TimeSpan(15, 31, 0)));
        }

        [Fact]
        public void Maps_timeonly_column_with_precision()
        {
            var mapping = CreateMapper().FindMapping(
                new RelationalTypeMappingInfo(
                    storeTypeName: SqlServerTimeOnlyTypeMappingSourcePlugin.SqlServerTypeName + "(3)",
                    storeTypeNameBase: SqlServerTimeOnlyTypeMappingSourcePlugin.SqlServerTypeName,
                    unicode: null,
                    size: null,
                    precision: null,
                    scale: null));

            AssertMapping<TimeOnly>(mapping);

            Assert.Equal("'15:31:01.048'", mapping.GenerateSqlLiteral(new TimeOnly(15, 31, 1, 48)));

            Assert.Equal("'15:31:01.048'", mapping.GenerateSqlLiteral(new TimeSpan(0, 15, 31, 1, 48)));
        }

        private static void AssertMapping<T>(
            RelationalTypeMapping mapping)
        {
            AssertMapping(typeof(T), mapping);
        }

        private static void AssertMapping(
            Type type,
            RelationalTypeMapping mapping)
        {
            Assert.Same(type, mapping.ClrType);
        }

        private static IRelationalTypeMappingSourcePlugin CreateMapper()
            => new SqlServerTimeOnlyTypeMappingSourcePlugin();
    }
}
