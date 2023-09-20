using System.Data.SQLite;
using PokeCardManager.Data;

namespace PokeCardManager.Classes;
public static class PC
{
    public static List<string> SubTypes     = new();
    public static List<string> SuperTypes   = new();
    public static List<string> ElementTypes = new();
    public static List<string> Rarities     = new();
    
    public static List<SetData> Sets        = new();
    public static List<FolderData> Folders  = new();
    public static List<CardData> Cards      = new();

    public static Dictionary<string, string> FolderTypes = new()
    {
        { "owned",    "Owned"    },
        { "wishlist", "Wishlist" },
        { "selling",  "Selling"  },
        { "list",     "List"     },
    };

    public static List<string> LSubTypes     => SubTypes;
    public static List<string> LSuperTypes   => SuperTypes;
    public static List<string> LElementTypes => ElementTypes;
    public static List<string> LRarities     => Rarities;


    public static bool AddFolder(FolderData folder)
    {

        if (Folders.Exists(f => f.name == folder.name))
            return false;

        var maxPos = Sqlite.GetInt($"SELECT MAX(sortIndex) FROM Folders WHERE folderType = '{folder.folderType}'");

        if (!Sqlite.Query(@"INSERT INTO Folders (name, folderType, sortIndex) VALUES (@name, @folderType, @sortIndex);",
            new SQLiteParameter("name", folder.name),
            new SQLiteParameter("folderType", folder.folderType),
            new SQLiteParameter("sortIndex", maxPos + 1)
            )) return false;

        Folders = Sqlite.GetFolders();

        return true;
    }

    public static bool SortFolder(FolderSort folder)
    {
        for (var i = Folders.Count; --i >= 0;)
        {
            if (Folders[i].id == folder.folderId)
            {
                Folders[i].sortIndex = folder.sortIndex;
                Folders[i].parentId  = folder.parentId;
            }
            else if (Folders[i].parentId == folder.parentId)
            {
                if (Folders[i].sortIndex >= folder.sortIndex) Folders[i].sortIndex++;
            }
            else continue;

            Sqlite.Query(@"UPDATE Folders SET sortIndex = @sortIndex, parentId = @parentId WHERE id = @id;",
                new SQLiteParameter("sortIndex", Folders[i].sortIndex),
                new SQLiteParameter("parentId",  Folders[i].parentId),
                new SQLiteParameter("id",        Folders[i].id));

        }

        IEnumerable<FolderData> SortedFolders = Folders.OrderBy(folder => folder.sortIndex);

        Folders = SortedFolders.ToList();

        return true;
    }

}
