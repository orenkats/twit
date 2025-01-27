using TwitterApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); 
});

// Register services
builder.Services.RegisterApplicationServices(builder.Configuration);

// Add logging configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure middleware pipeline
app.ConfigureApplicationPipeline();

// Run the application
app.Run();
