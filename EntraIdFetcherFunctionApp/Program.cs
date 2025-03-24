using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EntraIdFetcher.Interfaces;
using EntraIdFetcher.Helpers;
using EntraIdFetcher.Services;

var builder = FunctionsApplication.CreateBuilder(args);

// Configuring Azure Functions pipeline for HTTP trigger 
builder.ConfigureFunctionsWebApplication();

// Logging enable using Console and Debug (for local debugging) 
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

// Registering the configuration as a singleton shared as one instance and it can be injected anywhere
builder.Services.AddSingleton(builder.Configuration);

// Register custom services such as IGraphAuthProvider is the interface and GraphServiceClientProvider is the class implementing the logic
builder.Services.AddSingleton<IGraphAuthProvider, GraphServiceClientProvider>();
// Later usage in other services like IGraphAuthProvider provider = new GraphServiceClientProvider();

/*
 * Registers UserService as a Scoped service which means a new instance is created per function execution.
 * by taking authenticated graph object for fetching user level info
 * like public MyFunctionSample(IUserService userService) { ... }
 *
 */
builder.Services.AddScoped<IUserService>(provider =>
{
    var graphProvider = provider.GetRequiredService<IGraphAuthProvider>();
    var graphClient = graphProvider.GetGraphServiceClient();
    return new UserService(graphClient);
});

// Start the application by building it 
builder.Build().Run();
