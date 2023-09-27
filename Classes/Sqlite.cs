//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Sqlite
//
using System.Data;
using System.Data.SQLite;
using System.Text;
using PokeCardManager.Data;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;
using System.IO;
using System.Collections.Generic;
using System;

namespace PokeCardManager.Classes;
public static class Sqlite
{
    static string DatabasePath;
    static readonly string DatabaseName = "PokeCardManager.db";

    public static void Init()
    {
        DatabasePath = @"URI=file:" + Path.Combine(Config.DataPath, DatabaseName);

        CreateTables();
    }

    static void CreateTables(bool startFresh = false)
    {
        SQLiteConnection con = new(DatabasePath);

        con.Open();

        SQLiteTransaction transaction = con.BeginTransaction();
        StringBuilder sb = new StringBuilder();

        SQLiteCommand cmd;

        if (startFresh)
        {
            var Tables  = GetColumn<string>("SELECT name FROM sqlite_master WHERE type='table';");
            var Indexes = GetColumn<string>("SELECT name FROM sqlite_master WHERE type='index';");

            foreach (var tblName in Tables)
            {
                sb.Clear();
                sb.AppendFormat("DROP TABLE IF EXISTS {0};", tblName);
                cmd = new SQLiteCommand(sb.ToString(), con, transaction);
                cmd.ExecuteNonQuery();
            }

            foreach (var indexName in Indexes)
            {
                sb.Clear();
                sb.AppendFormat("DROP INDEX IF EXISTS {0};", indexName);
                cmd = new SQLiteCommand(sb.ToString(), con, transaction);
                cmd.ExecuteNonQuery();
            }
        }

        var tableSql = Utils.ReadResource("DBSchema.sql");

        cmd = new SQLiteCommand(tableSql, con, transaction)
        {
            CommandText = tableSql
        };

        cmd.ExecuteNonQuery();
        transaction.Commit();

        con.Close();
    }


    //  Function ImportSets
    public static bool ImportSets(List<Set> SetsData)
    {
        using SQLiteConnection con = new SQLiteConnection(DatabasePath);

        con.Open();

        SQLiteTransaction transaction = con.BeginTransaction();

        SQLiteCommand cmd;

        var query = @"INSERT OR IGNORE INTO Sets (id, name, series, printedTotal, Total, releaseDate, imgSymbol, imgLogo) 
                            VALUES (@id, @name, @series, @printedTotal, @Total, @releaseDate, @imgSymbol, @imgLogo);";

        foreach (var set in SetsData)
        {
            cmd = new SQLiteCommand(query, con, transaction);

            cmd.Parameters.Add(new SQLiteParameter("@id",           set.Id));
            cmd.Parameters.Add(new SQLiteParameter("@name",         set.Name));
            cmd.Parameters.Add(new SQLiteParameter("@series",       set.Series));
            cmd.Parameters.Add(new SQLiteParameter("@printedTotal", set.PrintedTotal));
            cmd.Parameters.Add(new SQLiteParameter("@Total",        set.Total));
            cmd.Parameters.Add(new SQLiteParameter("@releaseDate",  set.ReleaseDate.Replace("/", "-")));
            cmd.Parameters.Add(new SQLiteParameter("@imgSymbol",    set.Images.Symbol.ToString()));
            cmd.Parameters.Add(new SQLiteParameter("@imgLogo",      set.Images.Logo.ToString()));

            cmd.ExecuteNonQuery();
        }

        transaction.Commit();

        con.Close();

        return true;
    }


    //  Get Folders rows and return as a List<FolderData>
    public static List<FolderData> GetFolders()
    {
        List<FolderData> folders = new();

        using SQLiteConnection con = new SQLiteConnection(DatabasePath);

        con.Open();

        using var cmd = new SQLiteCommand("SELECT id, parentId, sortIndex, name, folderType, icon FROM Folders ORDER BY folderType, sortIndex, name", con);

        SQLiteDataReader r = cmd.ExecuteReader();

        while (r.Read())
        {
            folders.Add(new FolderData()
            {
                id         = r.GetInt32(0),
                parentId   = r.GetInt32(1),
                sortIndex  = r.GetInt32(2),
                name       = r.GetString(3),
                folderType = r.GetString(4),
                icon       = r.GetString(5)
            });
        }

        foreach (var folder in folders)
        {
            folder.CardMaps = new();

            using var cmd2 = new SQLiteCommand($"SELECT cardId, cost, date, quantity, options FROM FolderMap WHERE folderId = {folder.id}", con);

            SQLiteDataReader r2 = cmd2.ExecuteReader();

            while (r2.Read())
            {
                folder.CardMaps.Add(new FolderCardMap()
                {
                    cardId   = r2.GetInt32(0),
                    cost     = r2.GetDecimal(1),
                    date     = r2.GetDateTime(2),
                    quantity = r2.GetInt32(3),
                    options  = r2.GetString(4)
                });
            }
        }

        return folders;
    }


    //  Get Cards rows and return as a List<CardData>
    public static List<CardData> GetCards()
    {
        List<CardData> cards = new();

        using SQLiteConnection con = new SQLiteConnection(DatabasePath);

        con.Open();

        using (var cmd = new SQLiteCommand("SELECT * FROM Cards", con))
        {
            SQLiteDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                cards.Add(new CardData()
                {
                    rowId      = r.GetInt32(0),
                    id         = r.GetString(1),
                    name       = r.GetString(2),
                    supertype  = r.GetString(3),
                    setId      = r.GetInt32(4),
                    number     = r.GetInt32(5),
                    rarity     = r.GetString(6),
                    imgSmall   = r.GetString(7),
                    imgLarge   = r.GetString(8),
                    tcgUrl     = r.GetString(9),
                    tcgUrlReal = r.GetString(10),
                    cmUrl      = r.GetString(11),
                    apiJson    = r.GetString(12),
                });
            }
        }

        foreach (var card in cards)
        {
            //  Get Subtypes
            card.subTypes.AddRange(GetColumn<string>(@$"
                SELECT subtype 
                FROM Subtypes s, SubtypeMap sm 
                WHERE s.id = sm.subtypeId 
                    AND cardId = {card.rowId}"));
        }

        return cards;
    }



    //  Get Sets rows and return as a List<SetData>
    public static List<SetData> GetSets()
    {
        List<SetData> sets = new();

        using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
        {
            con.Open();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Sets ORDER BY name", con))
            {
                SQLiteDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    sets.Add(new SetData()
                    {
                        rowId        = r.GetInt32(0),
                        id           = r.GetString(1),
                        name         = r.GetString(2),
                        series       = r.GetString(3),
                        printedTotal = r.GetInt32(4),
                        Total        = r.GetInt32(5),
                        releaseDate  = r.GetDateTime(6),
                        imgSymbol    = r.GetString(7),
                        imgLogo      = r.GetString(8),
                    });
                }
            }

            con.Close();
        }

        return sets;
    }


    public static string GetString(string query)
    {
        using SQLiteConnection con = new SQLiteConnection(DatabasePath);

        con.Open();

        using SQLiteCommand cmd = new SQLiteCommand(query, con);

        var result = (string)cmd.ExecuteScalar()?.ToString();

        con.Close();

        if (result == null) return string.Empty;
        else return result.ToString();
    }

    public static int GetInt(string query) => (!int.TryParse(GetString(query), out var res)) ? 0 : res;


    public static T[] GetColumn<T>(string query)
    {
        List<T> column = new();

        using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
        {
            con.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(query, con))
            {
                SQLiteDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    column.Add(r.GetFieldValue<T>(0));
                }
            }

            con.Close();

        }

        return column.ToArray();
    }


    public static bool Query(SQLiteCommand query)
    {
        using SQLiteConnection con = new SQLiteConnection(DatabasePath);

        con.Open();

        query.Connection = con;

        var res = query.ExecuteNonQuery();

        con.Close();

        return (res >= 1);
    }


    public static bool Query(string query)
    {
        SQLiteCommand cmd = new SQLiteCommand()
        {
            CommandText = query,
        };

        return Query(cmd);
    }


    public static bool Query(string query, params SQLiteParameter[] parameters)
    {
        SQLiteCommand cmd = new SQLiteCommand()
        {
            CommandText = query,
        };

        foreach (SQLiteParameter parameter in parameters) cmd.Parameters.Add(parameter);

        return Query(cmd);
    }


    public static string GetQuery(SQLiteCommand cmd)
    {
        var CommandTxt = cmd.CommandText;

        foreach (SQLiteParameter parms in cmd.Parameters)
        {
            var val = string.Empty;

            if (parms.DbType.Equals(DbType.String) || parms.DbType.Equals(DbType.DateTime))
                val = "'" + Convert.ToString(parms.Value).Replace(@"\", @"\\").Replace("'", @"\'") + "'";

            if (parms.DbType.Equals(DbType.Int16)
                || parms.DbType.Equals(DbType.Int32)
                || parms.DbType.Equals(DbType.Int64)
                || parms.DbType.Equals(DbType.Decimal)
                || parms.DbType.Equals(DbType.Double)) val = Convert.ToString(parms.Value);

            var paramname = "@" + parms.ParameterName;

            CommandTxt = CommandTxt.Replace(paramname, val);
        }

        return (CommandTxt);
    }

}
