using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MinkyShopProject.Admin;
using Majorsoft.Blazor.Components.CssEvents;
using Majorsoft.Blazor.Components.Common.JsInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddCssEvents();
//builder.Services.AddJsInteropExtensions();

await builder.Build().RunAsync();
