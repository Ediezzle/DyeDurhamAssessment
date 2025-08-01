// See https://aka.ms/new-console-template for more information
// <summary>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NameSorter.Application;

/// App entry point
/// </summary>
public class Program
{
    static async Task Main(string[] args)
    {
        // Create host builder with DI container
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register services
                services.AddTransient<IFileService, FileService>();
                services.AddTransient<INameParsingService, NameParsingService>();
                services.AddTransient<INameSortingService, NameSortingService>();
                services.AddTransient<INameSortingApp, NameSortingApp>();

                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                });
            })
            .Build();

        try
        {
            // Resolve and run the application
            var app = host.Services.GetRequiredService<INameSortingApp>();
            await app.RunAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application error: {ex.Message}");
        }
        finally
        {
            await host.StopAsync();
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
