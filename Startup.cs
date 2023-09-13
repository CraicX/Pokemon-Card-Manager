using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeCardManager.Classes.Events;
using System.Runtime.InteropServices.JavaScript;

namespace PokeCardManager;
public static class Startup
{
    public static IServiceProvider Services
    {
        get; private set;
    }

    public static void Init()
    {

        //JSHost.ImportAsync("Interop", "/js/interop.js");

        var host = Host.CreateDefaultBuilder()
                       .ConfigureServices(WireupServices)
                       .Build();
        Services = host.Services;
    }

    private static void WireupServices(IServiceCollection services)
    {
        services.AddWpfBlazorWebView();
        services.AddSingleton<AddFolderEvent>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
    }
}
