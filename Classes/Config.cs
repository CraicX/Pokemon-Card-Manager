using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;


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

        Sqlite.Init();

        PokeAPI.Init();

        AddRecords();

        GetRecords();

        Browser.Initialize();

        Main.GridApp.Children.Add(Browser.CB);

        Grid.SetColumn(Browser.CB, 0);

    }

    private static async void AddRecords(bool forceRefresh = false)
    {

        //
        //  Check last time Subtypes was updated
        //
        var lastUpdate = Properties.Settings.Default.SubTypesUpdate; 
        var hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - lastUpdate) / 3600;
        
        if (forceRefresh || hours > 24*7 || Sqlite.GetString("SELECT COUNT(*) FROM Subtypes;") == "0")  
        {
            Debug.WriteLine("Refreshing SubTypes...");

            var subTypes = await PokeAPI.pokeClient.GetStringResourceAsync<SubTypes>();

            Sqlite.Query($"DELETE FROM Subtypes;");

            foreach (var subType in subTypes.SubType)
            {
                Sqlite.Query(@"INSERT INTO Subtypes (Name) VALUES (@subType);", 
                    new SQLiteParameter("subType", subType));
            }

            Properties.Settings.Default.SubTypesUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            Properties.Settings.Default.Save();
        }

        //
        //  Check last time Rarities was updated
        //
        lastUpdate = Properties.Settings.Default.RaritiesUpdate;
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - lastUpdate) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Rarities;") == "0")
        {
            Debug.WriteLine("Refreshing Rarities...");
            
            var rarities = await PokeAPI.pokeClient.GetStringResourceAsync<Rarities>();

            Sqlite.Query($"DELETE FROM Rarities;");
            
            foreach (var rarity in rarities.Rarity)
            {
                Sqlite.Query(@"INSERT INTO Rarities (Name) VALUES (@rarity);", 
                    new SQLiteParameter("rarity", rarity));
            }

            Properties.Settings.Default.RaritiesUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            Properties.Settings.Default.Save();
        }

        //
        //  Check last time SuperTypes was updated
        //
        lastUpdate = Properties.Settings.Default.SuperTypesUpdate;
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - lastUpdate) / 3600;

        if (forceRefresh || hours > 24 * 7 || Properties.Settings.Default.SuperTypes == "")
        {
            Debug.WriteLine("Refreshing SuperTypes...");

            //var superTypes = await PokeAPI.pokeClient.GetStringResourceAsync<SuperTypes>();

            //var SuperTypesConcat = string.Join(",", superTypes.SuperType);

            //Properties.Settings.Default.SuperTypes       = SuperTypesConcat;
            //Properties.Settings.Default.SuperTypesUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            //Properties.Settings.Default.Save();
        }

        //
        //  Check last time Types was updated
        //
        lastUpdate = Properties.Settings.Default.ElementTypesUpdate;
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - lastUpdate) / 3600;

        if (forceRefresh || hours > 24 * 7 || Properties.Settings.Default.ElementTypes == "")
        {
            Debug.WriteLine("Refreshing ElementTypes...");

            var elementTypes = await PokeAPI.pokeClient.GetStringResourceAsync<ElementTypes>();

            var ElementTypesConcat = string.Join(",", elementTypes.ElementType);

            Properties.Settings.Default.ElementTypes       = ElementTypesConcat;
            Properties.Settings.Default.ElementTypesUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            Properties.Settings.Default.Save();
        }

        //
        //  Check last time Sets were updated
        //
        lastUpdate = Properties.Settings.Default.SetsUpdate;
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - lastUpdate) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Sets;") == "0")
        {
            Debug.WriteLine("Refreshing Sets...");

            var cardSets = await PokeAPI.GetSetsFromAPI();

            Sqlite.ImportSets(cardSets);

            Properties.Settings.Default.SetsUpdate = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            Properties.Settings.Default.Save();
        }

    }


    //
    // === GET RECORDS === 
    //
    private static void GetRecords()
    {
        PC.SubTypes.AddRange(Sqlite.GetColumn<string>("SELECT Name FROM Subtypes ORDER BY Name;"));
        PC.Rarities.AddRange(Sqlite.GetColumn<string>("SELECT Name FROM Rarities ORDER BY Name;"));

        PC.SuperTypes.AddRange(Properties.Settings.Default.SuperTypes.Split(','));
        PC.ElementTypes.AddRange(Properties.Settings.Default.ElementTypes.Split(','));

        PC.Sets    = Sqlite.GetSets();
        PC.Cards   = Sqlite.GetCards();
        PC.Folders = Sqlite.GetFolders();
    }




    //
    // === DEFINE PATHS === 
    //
    private static void DefinePaths()
    {
        //  Set the App Paths & Create need directories

        RootPath    = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Config.AppName);
        AppPath     = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        CachePath   = Path.Combine(RootPath, "Cache");
        WebPath     = Path.Combine(AppPath,  "Web");
        TPLPath     = Path.Combine(WebPath,  "sections");
        WidgetPath  = Path.Combine(WebPath,  "widgets");
        DataPath    = Path.Combine(RootPath, "Data");

        foreach (var path in new string[] { CachePath, WebPath, TPLPath, WidgetPath, DataPath })
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);


    }





}
