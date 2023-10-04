//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  FolderTypes
//

namespace PokeCardManager.Data;
public class FolderTypes
{
    public string Name
    {
        get; set;
    }
    public string Title
    {
        get; set;
    }
    public bool Collapsed
    {
        get; set;
    }
    public int Order
    {
        get; set;
    }
}
