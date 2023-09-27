//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  FolderCardMap
//
using System;

namespace PokeCardManager.Data;
public class FolderCardMap
{
    public int cardId { get; set; }
    public decimal cost { get; set; }
    public DateTime date { get; set; }
    public int quantity { get; set; }
    public string options { get; set; }
}
