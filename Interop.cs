using Microsoft.JSInterop;

namespace BlazorSample.JavaScriptInterop;

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