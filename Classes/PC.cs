﻿//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  PC
//
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PokeCardManager.Data;

namespace PokeCardManager.Classes;
public static class PC
{
    public static List<string> SubTypes         = new();
    public static List<string> SuperTypes       = new();
    public static List<string> ElementTypes     = new();
    public static List<string> Rarities         = new();
    public static List<SetData> Sets            = new();
    public static List<FolderData> Folders      = new();
    public static List<CardData> Cards          = new();
    public static List<Filter> Filters          = new();
    public static List<FolderTypes> FolderTypes = new();
    public static List<FolderTypes> FolderTree  = new();

    public static List<string> LSubTypes     => SubTypes;
    public static List<string> LSuperTypes   => SuperTypes;
    public static List<string> LElementTypes => ElementTypes;
    public static List<string> LRarities     => Rarities;


    public static bool AddFolder(FolderData folder)
    {

        if (Folders.Exists(f => f.name == folder.name))
            return false;

        var maxPos = Sqlite.GetInt($"SELECT MAX(sortIndex) FROM Folders WHERE folderType = '{folder.folderType}'");

        if (!Sqlite.Query(@"INSERT INTO Folders (name, folderType, sortIndex, color) VALUES (@name, @folderType, @sortIndex, @color);",
            new SQLiteParameter("name", folder.name),
            new SQLiteParameter("folderType", folder.folderType),
            new SQLiteParameter("sortIndex", maxPos + 1),
            new SQLiteParameter("color", folder.color)
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

    public static List<FolderTypes> GetFolderTree()
    {
        FolderTree = new();

        foreach (var folderType in FolderTypes)
        {

            var folders = Folders.Where(f => f.folderType == folderType.Name).ToList();

            foreach( var folder in folders)
            {
                folder.childCount = Folders.Count(f => f.parentId == folder.id);

                if (folder.childCount > 0)
                {
                    folder.children = Folders.Where(f => f.parentId == folder.id).ToList();
                }
               
            }

            foreach (var folder in folders)
            {
                folderType.Folders = folders.Where(f => f.parentId == 0).ToList();
            }
            
            if( folderType.Folders.Count > 0 ) FolderTree.Add(folderType);
            
        }

        return FolderTree;

    }

    public static async Task<bool> GetSetImages()
    {
        Console.WriteLine("Checking Set Images");

        foreach (var set in Sets)
        {
            var symbolPath = $"{Config.WWWRootPath}\\img\\icons\\sets\\{set.id}.png";

            if (!File.Exists(symbolPath))
            {
                using var client = new HttpClient();

                var res = await client.GetAsync(set.imgSymbol);

                var bytes = await res.Content.ReadAsByteArrayAsync();

                await File.WriteAllBytesAsync(symbolPath, bytes);

                Console.WriteLine($"Saved set image for [ {set.id} ]");

            }
            
        }

        return true;

    }

}
