using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace Microsoft.EntityFrameworkCore.SqlServer.Scaffolding
{
    internal class SqlServerDateOnlyTimeOnlyCodeGeneratorPlugin : ProviderCodeGeneratorPlugin
    {
        public override MethodCallCodeFragment? GenerateProviderOptions()
        {
            var methodInfo = typeof(SqlServerDateOnlyTimeOnlyDbContextOptionsBuilderExtensions).GetRuntimeMethod(
                    nameof(SqlServerDateOnlyTimeOnlyDbContextOptionsBuilderExtensions.UseDateOnlyTimeOnly),
                    new[] { typeof(SqlServerDbContextOptionsBuilder) });

            return methodInfo != null ? new MethodCallCodeFragment(methodInfo) : null;
        }
    }
}
