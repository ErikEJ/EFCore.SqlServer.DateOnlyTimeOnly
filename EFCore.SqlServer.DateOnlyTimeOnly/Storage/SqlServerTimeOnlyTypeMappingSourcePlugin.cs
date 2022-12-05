using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class SqlServerTimeOnlyTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "time";

        public virtual RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(TimeOnly).IsAssignableFrom(clrType)
                   || SqlServerTypeName.Equals(storeTypeName, StringComparison.OrdinalIgnoreCase)
                ? new SqlServerTimeOnlyTypeMapping(SqlServerTypeName, System.Data.DbType.Time)
                : null;
        }
    }
}
