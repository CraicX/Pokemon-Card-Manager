using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace PokeCard;

public static class Sqlite
{
    static string DatabasePath;
    static readonly string DatabaseName = "PokeCard.db";

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
            var Tables = GetColumn<string>("SELECT name FROM sqlite_master WHERE type='table';");
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


    public static string GetString(string query)
    {
        using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
        {
            con.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, con))
            {
                var result = cmd.ExecuteScalar();
                
                if (result == null) return string.Empty;
                else return result.ToString();
            }
        }
    }

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

        }

        return column.ToArray();
    }


    public static bool Query(string query)
    {
        using (SQLiteConnection con = new SQLiteConnection(DatabasePath))
        {
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand(con)
            {
                CommandText = query,
            };

            cmd.ExecuteNonQuery();

        }

        return true;
    }

    public static string GetQuery(SQLiteCommand cmd)
    {
        var CommandTxt = cmd.CommandText;

        foreach (SQLiteParameter parms in cmd.Parameters)
        {
            var val = String.Empty;
            if (parms.DbType.Equals(DbType.String) || parms.DbType.Equals(DbType.DateTime))
                val = "'" + Convert.ToString(parms.Value).Replace(@"\", @"\\").Replace("'", @"\'") + "'";
            if (parms.DbType.Equals(DbType.Int16) || parms.DbType.Equals(DbType.Int32) || parms.DbType.Equals(DbType.Int64) || parms.DbType.Equals(DbType.Decimal) || parms.DbType.Equals(DbType.Double))
                val = Convert.ToString(parms.Value);
            var paramname = "@" + parms.ParameterName;
            CommandTxt = CommandTxt.Replace(paramname, val);
        }
        return (CommandTxt);
    }

}
