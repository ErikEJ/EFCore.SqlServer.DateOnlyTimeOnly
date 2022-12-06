using System;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Microsoft.EntityFrameworkCore.SqlServer
{
    public class DateOnlyTypeMappingTests
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
        public void Maps_dateonly_column()
        {
            var mapping = CreateMapper().FindMapping(
                new RelationalTypeMappingInfo(
                    storeTypeName: SqlServerDateOnlyTypeMappingSourcePlugin.SqlServerTypeName,
                    storeTypeNameBase: SqlServerDateOnlyTypeMappingSourcePlugin.SqlServerTypeName,
                    unicode: null,
                    size: null,
                    precision: null,
                    scale: null));

            AssertMapping<DateOnly>(mapping);
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
            => new SqlServerDateOnlyTypeMappingSourcePlugin();
    }
}
