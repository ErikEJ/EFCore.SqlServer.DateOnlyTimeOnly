using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace Microsoft.EntityFrameworkCore.SqlServer.Scaffolding
{
    internal class SqlServerDateOnlyTimeOnlyCodeGeneratorPlugin : ProviderCodeGeneratorPlugin
    {
        public override MethodCallCodeFragment GenerateProviderOptions()
        {
            return new MethodCallCodeFragment(
                typeof(SqlServerDateOnlyTimeOnlyDbContextOptionsBuilderExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbContextOptionsBuilderExtensions.UseDateOnlyTimeOnly),
                    new[] { typeof(SqlServerDbContextOptionsBuilder) }));
        }
    }
}
