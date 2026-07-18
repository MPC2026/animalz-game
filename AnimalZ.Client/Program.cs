using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AnimalZ.Services;
using AnimalZ.Client.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure Supabase from appsettings.json
var supabaseUrl = builder.Configuration["Supabase:Url"] ?? "https://jpjbclvdyduxhufmiouz.supabase.co";
var supabaseKey = builder.Configuration["Supabase:Key"] ?? "sb_publishable_FwqP90UCDweI8tiaTY3Vbg_FTVk92eO";

builder.Services.AddSingleton(new SupabaseService(supabaseUrl, supabaseKey));

await builder.Build().RunAsync();