using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class SqlServerDateOnlyTypeMapping : RelationalTypeMapping
    {
        private const string DateFormatConst = "'{0:yyyy-MM-dd}'";

        internal SqlServerDateOnlyTypeMapping(string storeType, Type clrType)
            : base(CreateRelationalTypeMappingParameters(storeType, clrType))
        {
        }

        protected SqlServerDateOnlyTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new SqlServerDateOnlyTypeMapping(parameters);
        }

        private static RelationalTypeMappingParameters CreateRelationalTypeMappingParameters(string storeType, Type clrType)
        {
            return new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(
                    clrType),
                storeType,
                StoreTypePostfix.Precision,
                System.Data.DbType.Date);
        }

        protected override void ConfigureParameter(DbParameter parameter)
        {
            base.ConfigureParameter(parameter);

            ((SqlParameter)parameter).SqlDbType = SqlDbType.Date;

            if (Size.HasValue
                && Size.Value != -1)
            {
                parameter.Size = Size.Value;
            }
        }

        protected override string SqlLiteralFormatString
        {
            get
            {
                return DateFormatConst;
            }
        }
    }
}
