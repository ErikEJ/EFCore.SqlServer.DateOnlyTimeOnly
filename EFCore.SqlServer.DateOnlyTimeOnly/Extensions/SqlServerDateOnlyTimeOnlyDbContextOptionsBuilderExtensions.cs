using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// DateOnly and TimeOnly specific extension methods for <see cref="SqlServerDbContextOptionsBuilder"/>.
    /// </summary>
    public static class SqlServerDateOnlyTimeOnlyDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Enable DateOnly and TimeOnly mappings.
        /// </summary>
        /// <param name="optionsBuilder">The builder being used to configure SQL Server.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static SqlServerDbContextOptionsBuilder UseDateOnlyTimeOnly(
            this SqlServerDbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsBuilder = ((IRelationalDbContextOptionsBuilderInfrastructure)optionsBuilder).OptionsBuilder;

            var extension = coreOptionsBuilder.Options.FindExtension<SqlServerDateOnlyTimeOnlyOptionsExtension>()
                ?? new SqlServerDateOnlyTimeOnlyOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)coreOptionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }
    }
}
