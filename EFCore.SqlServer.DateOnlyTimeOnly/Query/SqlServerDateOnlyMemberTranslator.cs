using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class SqlServerDateOnlyMemberTranslator : IMemberTranslator
{
    private static readonly Dictionary<string, string> DatePartMapping
        = new()
        {
            { nameof(DateOnly.Year), "year" },
            { nameof(DateOnly.Month), "month" },
            { nameof(DateOnly.DayOfYear), "dayofyear" },
            { nameof(DateOnly.Day), "day" },
        };

    private readonly ISqlExpressionFactory _sqlExpressionFactory;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public SqlServerDateOnlyMemberTranslator(
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
        MemberInfo member,
        Type returnType,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        var declaringType = member.DeclaringType;

        if (declaringType == typeof(DateOnly))
        {
            var memberName = member.Name;

            if (DatePartMapping.TryGetValue(memberName, out var datePart))
            {
                return _sqlExpressionFactory.Function(
                    "DATEPART",
                    new[] { _sqlExpressionFactory.Fragment(datePart), instance! },
                    nullable: true,
                    argumentsPropagateNullability: new[] { false, true },
                    returnType);
            }
        }

        return null;
    }
}
