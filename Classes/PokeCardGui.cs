using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.DevTools.Network;
using System.IO;

namespace PokeCard;
public static class PokeCardGui
{
    public static string RenderCards()
    {
        var html     = "";
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "poke-card-block.htm"));

        if (PokeAPI.CardResults.Count > 0)
        {
            foreach (var pokeCard in PokeAPI.CardResults)
            {
                html += Template.GetHtml(template, pokeCard);
            }
        }

        return html;
    }

}
