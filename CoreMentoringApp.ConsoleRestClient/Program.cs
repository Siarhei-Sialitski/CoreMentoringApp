using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreMentoringApp.ConsoleRestClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            await RunAsync();
        }

        static async Task RunAsync()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IEntitiesService, RestApiEntitiesService>();
                    services.AddTransient<IEntitiesHandler, ConsoleEntitiesHandler>();
                }).UseConsoleLifetime();

            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                string userInput = string.Empty;
                do
                {
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        switch (userInput.ToLower())
                        {
                            case "p":
                            {
                                var entitiesProviderService = services.GetRequiredService<IEntitiesService>();
                                var entitiesHandler = services.GetRequiredService<IEntitiesHandler>();
                                await entitiesHandler.HandleAsync(await entitiesProviderService.GetProductsAsync());
                                break;
                            }
                            case "c":
                            {
                                var entitiesProviderService = services.GetRequiredService<IEntitiesService>();
                                var entitiesHandler = services.GetRequiredService<IEntitiesHandler>();
                                await entitiesHandler.HandleAsync(await entitiesProviderService.GetCategoriesAsync());
                                break;
                            }
                        }
                    }
                    userInput = GetUserInput();

                } while (!userInput.Equals("E", StringComparison.OrdinalIgnoreCase));
            }
        }

        static string GetUserInput()
        {
            Console.WriteLine("Choose command:");
            Console.WriteLine("P - get all products");
            Console.WriteLine("C - get all categories");
            Console.WriteLine("E - exit");
            return Convert.ToString(Console.ReadLine());
        }
        
    }
}
