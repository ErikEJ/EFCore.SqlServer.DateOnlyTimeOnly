using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class SqlServerTimeOnlyTypeMapping : RelationalTypeMapping
    {
        private readonly string[] _timeFormats =
        {
            @"'{0:hh\:mm\:ss}'",
            @"'{0:hh\:mm\:ss\.F}'",
            @"'{0:hh\:mm\:ss\.FF}'",
            @"'{0:hh\:mm\:ss\.FFF}'",
            @"'{0:hh\:mm\:ss\.FFFF}'",
            @"'{0:hh\:mm\:ss\.FFFFF}'",
            @"'{0:hh\:mm\:ss\.FFFFFF}'",
            @"'{0:hh\:mm\:ss\.FFFFFFF}'"
        };

        public SqlServerTimeOnlyTypeMapping(
            string storeType,
            DbType? dbType = System.Data.DbType.Time,
            StoreTypePostfix storeTypePostfix = StoreTypePostfix.Precision,
            int? precision = null)
            : base(
                new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(typeof(TimeOnly)),
                    storeType,
                    storeTypePostfix,
                    dbType,
                    precision: precision))
        {
        }

        protected SqlServerTimeOnlyTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new SqlServerTimeOnlyTypeMapping(parameters);

        protected override void ConfigureParameter(DbParameter parameter)
        {
            base.ConfigureParameter(parameter);

            ((SqlParameter)parameter).SqlDbType = SqlDbType.Time;

            if (Precision.HasValue)
            {
                // Workaround for inconsistent use of precision/scale between EF and SqlClient for VarTime types
                parameter.Scale = unchecked((byte)Precision.Value);
            }
        }

        protected override string SqlLiteralFormatString
        {
            get
            {
                if (Precision.HasValue)
                {
                    var precision = Precision.Value;
                    if (precision <= 7
                        && precision >= 0)
                    {
                        return _timeFormats[precision];
                    }
                }

                return _timeFormats[7];
            }
        }

        protected override string GenerateNonNullSqlLiteral(object value)
            => value is TimeSpan timeSpan && timeSpan.Milliseconds == 0
                ? string.Format(
                    CultureInfo.InvariantCulture, _timeFormats[0], value) //handle trailing decimal separator when no fractional seconds
                : string.Format(CultureInfo.InvariantCulture, SqlLiteralFormatString, value);
    }
}
