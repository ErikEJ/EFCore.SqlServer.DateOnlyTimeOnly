using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Reflection;
using ExpressionExtensions = Microsoft.EntityFrameworkCore.Query.ExpressionExtensions;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class SqlServerDateOnlyTimeOnlyDateDiffFunctionsTranslator : IMethodCallTranslator
{
    private readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping
        = new()
        {
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(DateOnly), typeof(DateOnly) })!,
                "year"
            },
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(DateOnly?), typeof(DateOnly?) })!,
                "year"
            },
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(DateOnly), typeof(DateOnly) })!,
                "month"
            },
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(DateOnly?), typeof(DateOnly?) })!,
                "month"
            },
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffDay), 
                    new[] { typeof(DbFunctions), typeof(DateOnly), typeof(DateOnly) })!,
                "day"
            },
            {
                typeof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(DateOnly?), typeof(DateOnly?) })!,
                "day"
            },
        };

    private readonly ISqlExpressionFactory _sqlExpressionFactory;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public SqlServerDateOnlyTimeOnlyDateDiffFunctionsTranslator(
        ISqlExpressionFactory sqlExpressionFactory)
    {
        _sqlExpressionFactory = sqlExpressionFactory;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual SqlExpression? Translate(
        SqlExpression? instance,
        MethodInfo method,
        IReadOnlyList<SqlExpression> arguments,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        if (_methodInfoDateDiffMapping.TryGetValue(method, out var datePart))
        {
            var startDate = arguments[1];
            var endDate = arguments[2];
            var typeMapping = ExpressionExtensions.InferTypeMapping(startDate, endDate);

            startDate = _sqlExpressionFactory.ApplyTypeMapping(startDate, typeMapping);
            endDate = _sqlExpressionFactory.ApplyTypeMapping(endDate, typeMapping);

            return _sqlExpressionFactory.Function(
                "DATEDIFF",
                new[] { _sqlExpressionFactory.Fragment(datePart), startDate, endDate },
                nullable: true,
                argumentsPropagateNullability: new[] { false, true, true },
                typeof(int));
        }

        return null;
    }
}
