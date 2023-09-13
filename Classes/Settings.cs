namespace PokeCardManager.Classes;
public static class Settings
{
    public static string Get( string name, string defaultValue = "" )
    {
        var result = Sqlite.GetString( $"SELECT value FROM settings WHERE name = '{name}'" );

        return (result != null && result != string.Empty) ? result : defaultValue;
    }

    public static long Get(string name, long defaultValue = 0)
    {
        var result = Sqlite.GetString( $"SELECT value FROM settings WHERE name = '{name}'" );

        if (long.TryParse( result, out var value )) return value;

        return defaultValue;
    }

    public static void Set( string name, string value )
    {
        Sqlite.Query( $"REPLACE INTO settings (name, value) VALUES ('{name}', '{value}')" );
    }

    public static void Set(string name, long value)
    {
        Sqlite.Query($"REPLACE INTO settings (name, value) VALUES ('{name}', '{value}')");
    }

}
