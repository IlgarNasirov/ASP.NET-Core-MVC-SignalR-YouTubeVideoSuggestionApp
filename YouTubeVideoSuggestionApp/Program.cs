using FluentValidation.AspNetCore;
using Serilog;
using YouTubeVideoSuggestionApp.Hubs;
using YouTubeVideoSuggestionApp.IRepositories;
using YouTubeVideoSuggestionApp.Models;
using YouTubeVideoSuggestionApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddSignalR();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddDbContext<YouTubeVideoSuggestionDbContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");
app.UseExceptionHandler("/Error/Error500");
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Suggestion}/{Action=Index}");
app.MapHub<SuggestionHub>("/hubs/suggestion");
app.Run();