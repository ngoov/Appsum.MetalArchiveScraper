using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetalDbContext(this IServiceCollection serviceCollection)
        => serviceCollection.AddDbContext<MetalContext>(options => options.UseMetalDbSqlite());

    public static DbContextOptionsBuilder UseMetalDbSqlite(this DbContextOptionsBuilder options) 
        => options.UseSqlite("Data Source=C:\\temp\\metal.db", x => x.UseNodaTime());
}