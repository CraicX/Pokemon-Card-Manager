using CefSharp;
using CefSharp.DevTools.Network;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace PokeCard;

public class SchemeFactory : ISchemeHandlerFactory
{
    public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
    {

        var requrl        = request.Url.Replace("cefsharp", "cefsharp/Web");
        Uri uri              = new Uri(requrl);
        var fileName      = HttpUtility.UrlDecode(uri.AbsolutePath);
        var fileExtension = Path.GetExtension(fileName);
        var mimeType      = ResourceHandler.GetMimeType(fileExtension);


        switch (fileExtension)
        {
            case ".htm":
                return HandlerHtm(fileName);
            
            case ".app":
                return HandlerApp(fileName.Replace("/Web/", ""));

            case ".pdf":
                //Log.Information("PDF: {0}", uri.ToString());

                var pdfBytes = File.ReadAllBytes(fileName);

                return ResourceHandler.FromByteArray(pdfBytes, mimeType);

            default:
                return HandlerDefault(fileName, fileExtension);

        }
    }


    //
    // === HTM HANDLER === 
    //
    public static IResourceHandler HandlerHtm(string fileName)
    {
        var mimeType = ResourceHandler.GetMimeType(".htm");
        var htmlPath = Utils.Path(Config.AppPath, fileName);
        var html     = File.ReadAllText(htmlPath);

        html = Template.GetHtml(html);


        //var action = new Action(() => {
        //    Browser.ShowInspector();
        //});

        //if (!Config.Main.Dispatcher.CheckAccess()) Config.Main.Dispatcher.Invoke(action);
        //else Task.Run(action);

        return ResourceHandler.FromString(html, null, true, mimeType);

    }

    //
    // === APP HANDLER === 
    //
    public static IResourceHandler HandlerApp(string appName)
    {
        appName = appName.Replace(".app", "");

        var mimeType = ResourceHandler.GetMimeType(".htm");

        var html = "";

        if (appName == "render-cards")
        {
            html = PokeCardGui.RenderCards();
        }

        return ResourceHandler.FromString(html, null, true, mimeType);
    }


    //
    // === DEFAULT HANDLER === 
    //
    public static IResourceHandler HandlerDefault(string fileName, string fileExtension)
    {

        var filePath = Utils.Path(Config.AppPath, fileName);


        if (!File.Exists(filePath))
        {
            //Log.Information(" 404 !!!!!!! {2} -> {0} . {1}", fileName, fileExtension, filePath);
            return ResourceHandler.FromString("404: " + filePath, null, true, Cef.GetMimeType(".htm"));
        }

        return ResourceHandler.FromFilePath(
            filePath,
            mimeType: ResourceHandler.GetMimeType(fileExtension),
            autoDisposeStream: true
        );

    }
}
