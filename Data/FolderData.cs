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
    public int id { get; set; }
    public int parentId { get; set; }
    public int sortIndex { get; set; }
    public int childCount { get; set; } = 0;
    public string name { get; set; }
    public string folderType { get; set; }
    public string icon { get; set; }
    public string color { get; set; } = "#CCCCCC";
    public List<FolderCardMap> CardMaps { get; set; }
    public List<FolderData> children { get; set; } = new();

}

