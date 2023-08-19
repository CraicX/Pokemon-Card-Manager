using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CefSharp.DevTools.Page;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;

namespace PokeCard;
public static class Config
{
    public static MainWindow Main;

    //
    //  Define Paths
    //
    public static string RootPath    = "";
    public static string CachePath   = "";
    public static string AppPath     = "";
    public static string AvatarsPath = "";
    public static string DataPath    = "";
    public static string VoicesPath  = "";
    public static string ExportPath  = "";
    public static string WebPath     = "";
    public static string TPLPath     = "";
    public static string WidgetPath  = "";
    public static string TempPath    = "";
    public static string AppName     = "PokeCard";

    public static void RunSetup(MainWindow _main)
    {
        Main = _main;

        DefinePaths();


        //Main.GridApp.Children.Add(WB1.CB);
        //Grid.SetColumn(WB1.CB, 0);

        Sqlite.Init();

        PokeAPI.Init();

        AddRecords();

        //Browser WB1 = new Browser();
        Browser.Initialize();

        Main.GridApp.Children.Add(Browser.CB);

        Grid.SetColumn(Browser.CB, 0);

        Browser.ShowInspector();

    }

    private static async void AddRecords()
    {
        //  Add Records to DB
        var subTypeCount = Sqlite.GetString("SELECT COUNT(*) FROM Subtypes;");

        if (subTypeCount == "0")
        {
            var types = await PokeAPI.pokeClient.GetStringResourceAsync<SubTypes>();

            foreach (var subType in types.SubType)
            {
                Sqlite.Query($"INSERT INTO Subtypes (Name) VALUES ('{subType}');");
            }

        }

        Debug.WriteLine(subTypeCount);



    }

    //
    // === DEFINE PATHS === 
    //
    private static void DefinePaths()
    {
        //  Set the App Paths & Create need directories

        RootPath    = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Config.AppName);
        CachePath   = Path.Combine(RootPath, "Cache");
        AppPath     = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        WebPath     = Path.Combine(AppPath, "Web");
        TPLPath     = Path.Combine(WebPath, "sections");
        WidgetPath  = Path.Combine(WebPath, "widgets");
        DataPath    = Path.Combine(RootPath, "Data");

        foreach (var path in new string[] { CachePath, WebPath, TPLPath, WidgetPath, DataPath })
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);


    }





}
