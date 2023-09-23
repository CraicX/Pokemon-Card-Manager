using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PokeCardManager.Classes;
using PokeCardManager.Classes.Events;

namespace PokeCardManager;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Config.Init();

        InitializeComponent();

        Width  = Config.Settings.WindowWidth;
        Height = Config.Settings.WindowHeight;

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddSingleton<AddFolderEvent>();
        serviceCollection.AddSingleton<UpdateFiltersEvent>();
        serviceCollection.AddBlazorWebViewDeveloperTools();

        Resources.Add("services", serviceCollection.BuildServiceProvider());
    }

    public void WindowResized(object sender, RoutedEventArgs e)
    {
        
    }

    void Shutdown(object sender, EventArgs e)
    {
        Config.Settings.WindowWidth  = (int)Width;
        Config.Settings.WindowHeight = (int)Height;
        Config.Settings.Save();
    }


}
