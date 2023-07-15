using _51stBlazor.Discord;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Get bot token from configuration *appsettings.json*
var botToken = builder.Configuration.GetSection("DiscordSettings").GetValue<string>("DiscordBotToken");

// Check if the token is null or empty
if (string.IsNullOrEmpty(botToken))
{
    throw new InvalidOperationException("Discord bot token is not set. Please check your configuration.");
}

//Create DiscordBot Instance
var bot = new DiscordBot(botToken);
await bot.StartAsync(CancellationToken.None);

//Add bot services to DI
builder.Services.AddSingleton<DiscordBot>(bot);
builder.Services.AddSingleton<DiscordService>(provider => new DiscordService(provider.GetRequiredService<DiscordBot>()));
builder.Services.AddSingleton<DiscordRosterService>();
builder.Services.Configure<DiscordSettings>(builder.Configuration.GetSection("DiscordSettings"));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

