//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Interop
//
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PokeCardManager;


public static class MasterJS
{
    public static IJSRuntime JS;

    internal static ValueTask<object> Focus(IJSRuntime jsRuntime, ElementReference element)
    {
        return jsRuntime.InvokeAsync<object>("blazoredTypeahead.setFocus", element);
    }

    internal static ValueTask<object> AddKeyDownEventListener(IJSRuntime jsRuntime, ElementReference element)
    {
        return jsRuntime.InvokeAsync<object>("blazoredTypeahead.addKeyDownEventListener", element);
    }

    internal static ValueTask<object> OnOutsideClick(this IJSRuntime jsRuntime, ElementReference element, object caller, string methodName, bool clearOnFire = false)
    {
        return jsRuntime.InvokeAsync<object>("blazoredTypeahead.onOutsideClick", element, DotNetObjectReference.Create(caller), methodName, clearOnFire);
    }
}

