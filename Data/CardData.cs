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
    public int CardId { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Supertype { get; set; }
    public List<string> SubTypes { get; set; }
    public int SetId { get; set; }
    public string Number { get; set; }
    public string Rarity { get; set; }
    public string ImgSmall { get; set; }
    public string ImgLarge { get; set; }
    public string TcgUrl { get; set; }
    public string TcgUrlReal { get; set; }
    public string CmUrl { get; set; }
    public string ApiJson { get; set; }
}
