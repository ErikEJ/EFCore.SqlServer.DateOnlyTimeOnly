using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

internal class SqlServerDateOnlyTimeOnlyObjectToStringTranslator : IMethodCallTranslator
{
    private const int DefaultLength = 100;

    private static readonly Dictionary<Type, string> _typeMapping
        = new()
        {
            { typeof(DateOnly), $"varchar({DefaultLength})" },
            { typeof(TimeOnly), $"varchar({DefaultLength})" },
        };

    private readonly ISqlExpressionFactory _sqlExpressionFactory;

    internal SqlServerDateOnlyTimeOnlyObjectToStringTranslator(ISqlExpressionFactory sqlExpressionFactory)
    {
        _sqlExpressionFactory = sqlExpressionFactory;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method,
        IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        //SqlServerObjectToStringTranslator

        if (instance == null || method.Name != nameof(ToString) || arguments.Count != 0)
        {
            return null;
        }

        return _typeMapping.TryGetValue(instance.Type, out var storeType)
               ? _sqlExpressionFactory.Function(
                   "CONVERT",
                   new[] { _sqlExpressionFactory.Fragment(storeType), instance },
                   nullable: true,
                   argumentsPropagateNullability: new[] { false, true },
                   typeof(string))
               : null;
    }
}