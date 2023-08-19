using CefSharp;
using CefSharp.Wpf;
using System.Collections.Generic;

namespace PokeCard;

public static class Browser
{
    public static ChromiumWebBrowser CB;
    public static CefSettings CefConfig;
    public static JSHandler JS;
    public static List<string> ScriptQueue = new();


    public static ChromiumWebBrowser Initialize()
    {
        CefSettings BrowserSettings = new CefSettings()
        {
            UserAgent   = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36",
            LogSeverity = LogSeverity.Disable
        };

        //
        //  Define Custom CefBrowser Scheme 
        //
        BrowserSettings.RegisterScheme(new CefCustomScheme
        {
            SchemeName           = "lf",
            DomainName           = "cefsharp",
            SchemeHandlerFactory = new SchemeFactory()
        });

        if (!Cef.IsInitialized) Cef.Initialize(BrowserSettings);

        CB = new ChromiumWebBrowser("lf://cefsharp/index.htm")
        {
            
        };

        CB.FrameLoadEnd += CB_FrameLoadEnd;

        return CB;

    }

    private static void CB_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
    {
        JS ??= new JSHandler();

        while (ScriptQueue.Count > 0)
        {
            var script = ScriptQueue.Pop();

            CB.GetBrowser().MainFrame.ExecuteJavaScriptAsync(script);
        }
    }

    public static void FireJS(string javascript)
    {
        IBrowser B = CB.GetBrowser();
        IFrame MF  = B.MainFrame;
        
        if (MF.IsMain) MF.ExecuteJavaScriptAsync(javascript);
    }

    public static void ShowInspector()
    {
        if (CB.IsBrowserInitialized) CB.ShowDevTools();
    }
}
