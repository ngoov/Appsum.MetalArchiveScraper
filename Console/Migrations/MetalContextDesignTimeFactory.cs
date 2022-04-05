using Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Appsum.MetalArchiveScraper.Console.Migrations;

public class MetalContextDesignTimeFactory : IDesignTimeDbContextFactory<MetalContext>
{
    public MetalContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MetalContext>();
        optionsBuilder.UseMetalDbSqlite();

        return new MetalContext(optionsBuilder.Options);
    }

}