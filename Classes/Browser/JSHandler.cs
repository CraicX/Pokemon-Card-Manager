using System.Threading.Tasks;
using System;
using CefSharp;
using CefSharp.DevTools.Network;
using CefSharp.JavascriptBinding;
using System.Windows.Threading;

namespace PokeCard;

public class JSHandler
{
    public JSHandler()
    {
        Init();
    }

    public void Init()
    {
        Browser.CB.JavascriptObjectRepository.ResolveObject += (sender, e) =>
        {
            IJavascriptObjectRepository repo = e.ObjectRepository;

            if (e.ObjectName == "boundAsync")
            {
                BindingOptions bindingOptions = BindingOptions.DefaultBinder;

                repo.NameConverter = new CamelCaseJavascriptNameConverter();

                repo.Register("boundAsync", new BoundObject(), options: bindingOptions);
            }
        };
    }


    public class BoundObject
    {
        public MainWindow W1 => Config.Main;

        public void CloseApp()
        {
            Run(new Action(() => { W1.Close(); }));
        }

        public void MaximizeApp()
        {
            Run(new Action(() =>
            {
                W1.WindowState = W1.WindowState == System.Windows.WindowState.Maximized
                               ? System.Windows.WindowState.Normal
                               : System.Windows.WindowState.Maximized;
            }));
        }

        public void MinimizeApp()
        {
            Run(new Action(() => {
                W1.WindowState = System.Windows.WindowState.Minimized;
            }));
        }

        public void OpenDebug()
        {
            Browser.CB.ShowDevTools();
        }

        public void SavePage()
        {
            Utils.SavePage("page-dump.htm");
        }

        public void PostPayload(string json)
        {
            _ = new Japi(json);
        }

        public string PostBack(string json)
        {
            Japi japi = new Japi(json);

            return japi.Response;
        }

        public void Run(Action action)
        {
            if (!Config.Main.Dispatcher.CheckAccess()) Config.Main.Dispatcher.Invoke(action);
            else Task.Run(action);
        }

    }
}
