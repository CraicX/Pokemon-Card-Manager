using System.Windows;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using PokeCardManager.Classes;

namespace PokeCardManager;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Config.Init();

        Resources.Add("services", Startup.Services);

        

        InitializeComponent();
        
        
        //if (Startup.Services != null)
        //{
        //    blazorWebView.Services = Startup.Services;
        //    blazorWebView.RootComponents.Add(new Microsoft.AspNetCore.Components.WebView.Wpf.RootComponent() { Selector = "#app" });
        //}

        //blazorWebView.HostPage = "wwwroot/index2.html";
        //blazorWebView.WebView.Reload();
    }
}
