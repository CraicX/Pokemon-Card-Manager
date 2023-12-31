﻿//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Settings 
//
using System;
using Newtonsoft.Json.Linq;

namespace PokeCardManager.Classes;
public class Settings
{
    public string Theme { get; set; }             = "dark-theme";
    public int WindowWidth { get; set; }          = 1000;
    public int WindowHeight { get; set; }         = 600;
    public int PerPage { get; set; }              = 30;
    public string ShowAPIKeyWarning { get; set; } = "true";
    public string AnimateLogo { get; set; }       = "true";
    public string AddCardEffects { get; set; }    = "true";
    public string APIKey { get; set; }            = "";
    public long SubTypesUpdated { get; set; }     = 0;
    public long RaritiesUpdated { get; set; }     = 0;
    public long SuperTypesUpdated { get; set; }   = 0;
    public long ElementTypesUpdated { get; set; } = 0;
    public long SetsUpdated { get; set; }         = 0;
    public string[] SuperTypes { get; set; }      = { "Pokémon", "Trainer", "Energy" };
    public string[] ElementTypes
    {
        get; set;
    } = {"Colorless", "Darkness", "Dragon", "Fairy", "Fighting", "Fire", "Grass", "Lightning",
            "Metal", "Psychic", "Water" };


    public static string Get( string name, string defaultValue = "" )
    {
        var result = Sqlite.GetString( $"SELECT value FROM settings WHERE name = '{name}'" );

        return (result != null && result != string.Empty) ? result : defaultValue;
    }

    public static long Get(string name, long defaultValue = 0)
    {
        var result = Sqlite.GetString( $"SELECT value FROM settings WHERE name = '{name}'" );

        return long.TryParse( result, out var value ) ? value : defaultValue;
    }

    public static void Set( string name, string value )
    {
        Sqlite.Query( $"REPLACE INTO settings (name, value) VALUES ('{name}', '{value}')" );
    }

    public static void Set(string name, long value)
    {
        Sqlite.Query($"REPLACE INTO settings (name, value) VALUES ('{name}', '{value}')");
    }

    

    public void Save()
    {
        var json = JObject.FromObject( this );
        
        var jsonStr = json.ToString();
        
        Set( "Settings", jsonStr );
    }

    public static Settings Load()
    {
        var jsonStr = Get("Settings", "");

        if (jsonStr == "")
        {
            return new Settings();
        }

        try
        {
            var json     = JObject.Parse(jsonStr);
            var settings = json.ToObject<Settings>();
            return settings;
        }
        catch (Exception ex) { Console.WriteLine(ex); }

        return new Settings();
    }





}