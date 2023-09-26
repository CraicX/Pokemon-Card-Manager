using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SuperTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;


namespace PokeCardManager.Classes;
public static class Config
{

    //
    //  Define Paths
    //
    public static string RootPath    = "";
    public static string WWWRootPath = "";
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
    public static Settings Settings  = new();

    public static async void Init()
    {
        DefinePaths();

        Sqlite.Init();

        Settings = Settings.Load();

        PokeAPI.Init();

        AddRecords();

        await GetRecords();

    }

    private static async void AddRecords(bool forceRefresh = false)
    {
        //
        //  Check last time Subtypes was updated
        //
        var hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SubTypesUpdated) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Subtypes;") == "0")
        {
            Debug.WriteLine("Refreshing SubTypes...");

            var subTypes = await PokeAPI.pokeClient.GetStringResourceAsync<SubTypes>();

            Sqlite.Query($"DELETE FROM Subtypes;");

            foreach (var subType in subTypes.SubType)
            {
                Sqlite.Query(@"INSERT INTO Subtypes (Name) VALUES (@subType);",
                    new SQLiteParameter("subType", subType));
            }

            Settings.SubTypesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time Rarities was updated
        //
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.RaritiesUpdated) / 3600;

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

            Settings.RaritiesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time SuperTypes was updated
        //
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SuperTypesUpdated) / 3600;

        var SuperTypesJoined = Settings.Get("SuperTypes", "");

        if (forceRefresh || hours > 24 * 7 || SuperTypesJoined == "")
        {
            Debug.WriteLine("Refreshing SuperTypes...");

            var superTypes = await PokeAPI.pokeClient.GetStringResourceAsync<SuperTypes>();

            Settings.SuperTypes        = superTypes.SuperType.ToArray();
            Settings.SuperTypesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time Types was updated
        //
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.ElementTypesUpdated) / 3600;

        var ElementTypesJoined = Settings.Get("ElementTypes", "");
        
        if (forceRefresh || hours > 24 * 7 || ElementTypesJoined == "")
        {
            Debug.WriteLine("Refreshing ElementTypes...");

            var elementTypes = await PokeAPI.pokeClient.GetStringResourceAsync<ElementTypes>();

            Settings.ElementTypes        = elementTypes.ElementType.ToArray();
            Settings.ElementTypesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time Sets were updated
        //
        hours      = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SetsUpdated) / 3600;
        
        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Sets;") == "0")
        {
            Debug.WriteLine("Refreshing Sets...");

            var cardSets = await PokeAPI.GetSetsFromAPI();

            Sqlite.ImportSets(cardSets);

            Settings.SetsUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

    }


    //
    // === GET RECORDS === 
    //
    private static async Task<bool> GetRecords()
    {
        PC.SubTypes.AddRange(Sqlite.GetColumn<string>("SELECT Name FROM Subtypes ORDER BY Name;"));
        PC.Rarities.AddRange(Sqlite.GetColumn<string>("SELECT Name FROM Rarities ORDER BY Name;"));

        PC.SuperTypes.AddRange( Settings.Get("SuperTypes", "Pokémon,Trainer,Energy").Split(',') );
        PC.ElementTypes.AddRange( Settings.Get("ElementTypes", "Colorless,Darkness,Dragon,Fairy,Fighting,Fire,Grass,Lightning,Metal,Psychic,Water").Split(',') );

        PC.Sets    = Sqlite.GetSets();
        PC.Cards   = Sqlite.GetCards();
        PC.Folders = Sqlite.GetFolders();

        await PC.GetSetImages();

        return true;
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
        WebPath     = Path.Combine(AppPath, "Web");
        TPLPath     = Path.Combine(WebPath, "sections");
        WidgetPath  = Path.Combine(WebPath, "widgets");
        DataPath    = Path.Combine(RootPath, "Data");
        WWWRootPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");

        foreach (var path in new string[] { CachePath, WebPath, TPLPath, WidgetPath, DataPath })
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

    }

}
