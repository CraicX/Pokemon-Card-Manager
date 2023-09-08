using System.Collections.Generic;
using System.Data.SQLite;

namespace PokeCard;
public static class PC
{
    public static List<string> SubTypes     = new();
    public static List<string> SuperTypes   = new();
    public static List<string> ElementTypes = new();
    public static List<string> Rarities     = new();
    
    public static List<SetData> Sets        = new();
    public static List<FolderData> Folders  = new();
    public static List<CardData> Cards      = new();


    public static bool AddFolder(FolderData folder)
    {

        if (Folders.Exists(f => f.name == folder.name))
            return false;

        if (!Sqlite.Query(@"INSERT INTO Folders (name, folderType) VALUES (@name, @folderType);",
            new SQLiteParameter("name", folder.name), 
            new SQLiteParameter("folderType", folder.folderType))) return false;

        Folders = Sqlite.GetFolders();

        return true;
    }

}
