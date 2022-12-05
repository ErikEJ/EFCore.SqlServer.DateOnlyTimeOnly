using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// EntityFrameworkCore.SqlServer.DateOnlyTimeOnly extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SqlServerDateOnlyTimeOnlyServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services required for DateOnly and TimeOnly support in the SQL Server provider for Entity Framework.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddEntityFrameworkSqlServerDateOnlyTimeOnly(
            this IServiceCollection serviceCollection)
        {
            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IRelationalTypeMappingSourcePlugin, SqlServerDateOnlyTypeMappingSourcePlugin>();

            return serviceCollection;
        }
    }
}
