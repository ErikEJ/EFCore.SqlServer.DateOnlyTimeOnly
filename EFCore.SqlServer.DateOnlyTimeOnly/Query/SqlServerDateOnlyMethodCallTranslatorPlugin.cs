using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class SqlServerDateOnlyMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public SqlServerDateOnlyMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory,
            IRelationalTypeMappingSource typeMappingSource)
        {
            Translators = new IMethodCallTranslator[]
            {
                new SqlServerDateOnlyMethodTranslator(sqlExpressionFactory, typeMappingSource)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
