using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddSingleton<AddFolderEvent>();
        serviceCollection.AddSingleton<UpdateFiltersEvent>();
        serviceCollection.AddBlazorWebViewDeveloperTools();

        Resources.Add("services", serviceCollection.BuildServiceProvider());


    }
}
