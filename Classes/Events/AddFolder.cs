using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PokeCardManager.Data;
using PokeCardManager.Classes;

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