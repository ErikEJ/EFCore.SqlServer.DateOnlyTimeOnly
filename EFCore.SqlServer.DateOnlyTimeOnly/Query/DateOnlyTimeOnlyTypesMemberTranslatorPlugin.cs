using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class DateOnlyTimeOnlyTypesMemberTranslatorPlugin : SqlServerMemberTranslatorProvider
    {
        public DateOnlyTimeOnlyTypesMemberTranslatorPlugin(
            RelationalMemberTranslatorProviderDependencies dependencies,
            IRelationalTypeMappingSource typeMappingSource)
            : base(dependencies, typeMappingSource)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(
                new IMemberTranslator[]
                {
                    new SqlServerDateOnlyMemberTranslator(sqlExpressionFactory, typeMappingSource),
                    new SqlServerTimeOnlyMemberTranslator(sqlExpressionFactory),
                });
        }
    }
}
