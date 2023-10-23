//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  MainWindow
//
using System;
using System.Windows;
using Microsoft.AspNetCore.Components.WebView.Wpf;
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
        serviceCollection.AddSingleton<MainWindow>();

        //  enable routing
        Resources.Add("services", serviceCollection.BuildServiceProvider());
    }

    void Shutdown(object sender, EventArgs e)
    {
        Config.Settings.WindowWidth  = (int)Width;
        Config.Settings.WindowHeight = (int)Height;
        Config.Settings.Save();
    }

}
