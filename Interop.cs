//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  Interop
//
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PokeCardManager;

public partial class JsInterop : IDisposable
{
    private readonly IJSRuntime js;

    public JsInterop(IJSRuntime js)
    {
        this.js = js;
    }

    public async Task ShowAlert(string msg, string alertType="success")
    {
        await js.InvokeVoidAsync("showAlert", msg, alertType);
    }

    public void Dispose()
    {
    }
}

public static class Interop
{
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

