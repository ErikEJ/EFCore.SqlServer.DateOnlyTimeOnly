using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class SqlServerTimeOnlyTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "time";

        public virtual RelationalTypeMapping? FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeNameBase;

            return typeof(TimeOnly).IsAssignableFrom(clrType)
                   || SqlServerTypeName.Equals(storeTypeName, StringComparison.OrdinalIgnoreCase)
                ? new SqlServerTimeOnlyTypeMapping(SqlServerTypeName, System.Data.DbType.Time, StoreTypePostfix.Precision, GetPrecision(mappingInfo.StoreTypeName))
                : null;
        }

        private int? GetPrecision(string? storeTypeName)
        {
            int? precision = null;

            if (!string.IsNullOrEmpty(storeTypeName))
            {
                var openParen = storeTypeName.IndexOf("(", StringComparison.Ordinal);
                if (openParen >= 0)
                {
                    precision = int.Parse(storeTypeName.Substring(openParen + 1, 1), CultureInfo.InvariantCulture);
                }
            }

            return precision;
        }
    }
}
