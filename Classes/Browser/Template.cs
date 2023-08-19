using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PokeCard;

public static class Template
{
    public static string GetHtml(string html, object obj = null)
    {
        MatchCollection MM = Regex.Matches(html, @"\[\[(\w+)=([^\]]+)\]\]");

        foreach (Match match in MM)
        {
            var swapText = "";
            var path = "";

            var command = match.Groups[1].Value;
            var item = match.Groups[2].Value;

            switch (command)
            {
                case "inc":
                    path =  Utils.Path(Config.TPLPath, item);
                    if (File.Exists(path)) swapText = GetHtml(File.ReadAllText(path));
                    break;

                case "widget":
                    path = Utils.Path(Config.WidgetPath, item);
                    if (File.Exists(path)) swapText = GetHtml(File.ReadAllText(path));
                    break;

                case "obj":
                    swapText = obj.GetType().GetProperty(item).GetValue(obj, null).ToString();
                    break;

                case "var":
                    swapText = obj.ToString();
                    break;

                case "func":
                    var parts = item.Split('.');
                    Type t         =Type.GetType("PokeCard." + parts[0]);
                    MethodInfo mi  = t.GetMethod(parts[1]);
                    swapText       = (string)mi.Invoke(null, null);
                    break;

                case "gv":
                    swapText = Globals.Get(item).ToString();
                    break;

            }

            html = html.Replace(match.Groups[0].Value, swapText);

        }

        return html;
    }

    public static string QueJS(string html)
    {
        MatchCollection MM = Regex.Matches(html, @"\[\[script\]\](.*?)\[\[/script\]\]", RegexOptions.Singleline);

        foreach (Match match in MM)
        {
            Browser.ScriptQueue.Add(match.Groups[1].Value);

            html = html.Replace(match.Groups[0].Value, string.Empty);
        }

        return html;
    }

}