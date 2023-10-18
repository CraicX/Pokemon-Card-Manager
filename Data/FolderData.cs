//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  FolderData
//
using System.Collections.Generic;

namespace PokeCardManager.Data;
public class FolderData
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int SortIndex { get; set; }
    public int ChildCount { get; set; } = 0;
    public string Name { get; set; }
    public string FolderType { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; } = "#CCCCCC";
    public List<FolderCardMap> CardMaps { get; set; }
    public List<FolderData> Children { get; set; } = new();

}

