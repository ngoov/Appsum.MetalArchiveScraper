using Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Appsum.MetalArchiveScraper.Console.Migrations;

public class MetalContextDesignTimeFactory : IDesignTimeDbContextFactory<MetalContext>
{
    public MetalContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MetalContext>();
        optionsBuilder.UseSqlite("Data Source=c:\\temp\\metal.db");

        return new MetalContext(optionsBuilder.Options);
    }

}