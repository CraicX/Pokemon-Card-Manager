//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  App
//
using System.Windows;
using System.Windows.Controls;
using static PokeCardManager.Startup;

namespace PokeCardManager;
public partial class App : Application
{




    //public EnvDTE.DTE DTE
    //{
    //    [System.Runtime.InteropServices.DispId(2)]
    //    get;
    //}

    public App()
    {

        Init();

        //  If in debug environment, clear the immediate windows

    }
}
