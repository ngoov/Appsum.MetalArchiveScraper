// See https://aka.ms/new-console-template for more information

using Data;

using Microsoft.Extensions.DependencyInjection;

using Scraper;

var services = new ServiceCollection();
services.AddHttpClient<IMetalStormService, MetalStormService>();
services.AddTransient<Scraper.Scraper>();
services.AddMetalDbContext();
ServiceProvider sp = services.BuildServiceProvider();

var scraper = sp.GetRequiredService<Scraper.Scraper>();

await scraper.Run();