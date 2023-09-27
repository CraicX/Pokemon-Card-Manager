//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  UpdateFiltersEvent
//
using System;
using System.Linq;
using PokeCardManager.Data;

namespace PokeCardManager.Classes.Events;
public class UpdateFiltersEvent
{
    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public bool AddFilter(Filter filter)
    {
        if (PC.Filters.Contains(filter)) return false;
        
        PC.Filters.Add(filter);

        NotifyStateChanged();

        return true;
    }


    public bool RemoveFilter(Filter filter)
    {
        for (var i = PC.Filters.Count; --i >= 0;)
        {
            if (PC.Filters[i].Type == filter.Type && PC.Filters[i].Value == filter.Value)
            {
                PC.Filters.RemoveAt(i);
                NotifyStateChanged();
                return true;
            }
        }

        return false;
    }


    public bool RemoveFilter(int filterHash)
    {
        var filter = PC.Filters.FirstOrDefault(f => f.Hash == filterHash);

        if (filter != null)
        {
            PC.Filters.Remove(filter);
            
            NotifyStateChanged();
            
            return true;
        }

        return false;
    }

}
