using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class SqlServerDateOnlyTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "date";

        public virtual RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(DateOnly).IsAssignableFrom(clrType)
                   || SqlServerTypeName.Equals(storeTypeName, StringComparison.OrdinalIgnoreCase)
                ? new SqlServerDateOnlyTypeMapping(SqlServerTypeName, clrType ?? typeof(DateOnly))
                : null;
        }
    }
}
