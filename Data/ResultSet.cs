//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  ResultSet
//
namespace PokeCardManager.Data;
public class ResultSet
{
    public int Count { get; set; }      = 0;
    public int TotalCount { get; set; } = 0;
    public int Page { get; set; }       = 0;
    public int PageSize { get; set; }   = 0;
    public bool FromCache { get; set; } = false;
}
