//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  SetData
//
using System;

namespace PokeCardManager.Data;
public class SetData
{
    public int rowId { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string series { get; set; }
    public int printedTotal { get; set; }
    public int Total { get; set; }
    public DateTime releaseDate { get; set; }
    public string imgSymbol { get; set; }
    public string imgLogo { get; set; }
}
