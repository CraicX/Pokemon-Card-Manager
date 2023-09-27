//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  AddFolderEvent
//
using System;
using PokeCardManager.Data;

namespace PokeCardManager.Classes.Events;

public class AddFolderEvent
{
    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public bool AddFolder(FolderData folder)
    {

        if (PC.AddFolder( folder )) { 

            NotifyStateChanged();

            return true;

        }

        return false;
    }

}