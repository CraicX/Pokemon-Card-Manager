//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  CardData
//
using System.Collections.Generic;

namespace PokeCardManager.Data;
public class CardData
{
    public int rowId { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string supertype { get; set; }
    public List<string> subTypes { get; set; }
    public int setId { get; set; }
    public int number { get; set; }
    public string rarity { get; set; }
    public string imgSmall { get; set; }
    public string imgLarge { get; set; }
    public string tcgUrl { get; set; }
    public string tcgUrlReal { get; set; }
    public string cmUrl { get; set; }
    public string apiJson { get; set; }
}
