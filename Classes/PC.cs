using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
