//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Config
//
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;

using PokeCardManager.Data;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SuperTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;
using System.Windows.Media;
using System.Linq;

namespace PokeCardManager.Classes;
public static class Config
{

    //
    //  Define Paths
    //
    public static string RootPath = "";
    public static string WWWRootPath = "";
    public static string AppPath = "";
    public static string DataPath = "";
    public static string AppName = "PokeCard";
    public static Settings Settings = new();

    [Obsolete]
    public static async void Init()
    {


        //  Add console logging using Microsoft Extensions Logging

        Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

        DefinePaths();

        Sqlite.Init();

        Settings = Settings.Load();

        DefineFolderTypes();

        PokeAPI.Init();

        AddRecords();

        await GetRecords();

    }

    private static async void AddRecords(bool forceRefresh = false)
    {
        //
        //  Check last time Subtypes was updated
        //
        var hours = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SubTypesUpdated) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Subtypes;") == "0")
        {
            Log.Information("Refreshing SubTypes...");

            var subTypes = await PokeAPI.PokeClient.GetStringResourceAsync<SubTypes>();

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
        hours = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.RaritiesUpdated) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Rarities;") == "0")
        {
            Log.Information("Refreshing Rarities...");

            var rarities = await PokeAPI.PokeClient.GetStringResourceAsync<Rarities>();

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
        hours = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SuperTypesUpdated) / 3600;

        //var SuperTypesJoined = Settings.Get("SuperTypes", "");
        var SuperTypesJoined = string.Join(',', Settings.SuperTypes);

        if (forceRefresh || hours > 24 * 7 || SuperTypesJoined == "")
        {
            Log.Information("Refreshing SuperTypes...");

            var superTypes    = await PokeAPI.PokeClient.GetStringResourceAsync<SuperTypes>();

            Settings.SuperTypes        = superTypes.SuperType.ToArray();
            Settings.SuperTypesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time Types was updated
        //
        hours = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.ElementTypesUpdated) / 3600;

        //var ElementTypesJoined = Settings.Get("ElementTypes", "");
        var ElementTypesJoined = string.Join(',', Settings.ElementTypes); 

        if (forceRefresh || hours > 24 * 7 || ElementTypesJoined == "")
        {
            Log.Information("Refreshing ElementTypes...");

            var elementTypes   = await PokeAPI.PokeClient.GetStringResourceAsync<ElementTypes>();

            Settings.ElementTypes        = elementTypes.ElementType.ToArray();
            Settings.ElementTypesUpdated = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            Settings.Save();
        }

        //
        //  Check last time Sets were updated
        hours = (DateTime.Now.Ticks / TimeSpan.TicksPerSecond - Settings.SetsUpdated) / 3600;

        if (forceRefresh || hours > 24 * 7 || Sqlite.GetString("SELECT COUNT(*) FROM Sets;") == "0")
        {
            Log.Information("Refreshing Sets...");

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

        PC.SuperTypes.AddRange(Settings.Get("SuperTypes", "Pokémon,Trainer,Energy").Split(','));
        PC.ElementTypes.AddRange(Settings.Get("ElementTypes", "Colorless,Darkness,Dragon,Fairy,Fighting,Fire,Grass,Lightning,Metal,Psychic,Water").Split(','));

        PC.Sets = Sqlite.GetSets();
        PC.Cards = Sqlite.GetCards();
        PC.Folders = Sqlite.GetFolders();

        await PC.GetSetImages();

        return true;
    }

    //
    // === DEFINE FOLDER TYPES === 
    //
    private static void DefineFolderTypes()
    {
        PC.FolderTypes.Add(new FolderTypes
        {
            Name      = "owned",
            Title     = "Owned",
            Collapsed = true,
            Order     = 1,
        });

        PC.FolderTypes.Add(new FolderTypes
        {
            Name      = "wishlist",
            Title     = "Wishlist",
            Collapsed = false,
            Order     = 3,
        });

        PC.FolderTypes.Add(new FolderTypes
        {
            Name      = "selling",
            Title     = "Selling",
            Collapsed = false,
            Order     = 4,
        });

        PC.FolderTypes.Add(new FolderTypes
        {
            Name      = "list",
            Title     = "List",
            Collapsed = false,
            Order     = 2,
        });

        PC.FolderTypes = PC.FolderTypes.OrderBy(o => o.Order).ToList();
    }


    //
    // === DEFINE PATHS === 
    //
    private static void DefinePaths()
    {
        //  Set the App Paths & Create need directories

        RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Config.AppName);
        AppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        DataPath = Path.Combine(RootPath, "Data");
        WWWRootPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");

        foreach (var path in new string[] { DataPath })
        {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }
    }

}
