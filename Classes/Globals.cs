using System.Collections.Generic;

namespace PokeCard;

public static class Globals
{
    public static Dictionary<string, string> DicStr = new();

    public static string Get(string key)
    {
        if (DicStr.TryGetValue(key, out var val)) return val;
        
        return "";
    }

    public static void Set(string key, string val)
    {
        if (DicStr.ContainsKey(key))
        {
            DicStr[key] = val;
        }
        else
        {
            DicStr.Add(key, val);
        }

    }
}
