using System.Collections.Generic;
using System.IO;

namespace PokeCard;

public static class PCInterface
{
    public static async void ExecuteSearch(Dictionary<string, string> data)
    {
        _ = await PokeAPI.CardSearch(data["query"]);

        Browser.FireJS(@"jFetchCards('render-cards.app', '.page-content');");
    }

    public static string RenderCards()
    {
        var html        = "";
        var template    = File.ReadAllText(Utils.Path(Config.WidgetPath, "poke-card-block.htm"));
        var templateImg = File.ReadAllText(Utils.Path(Config.WidgetPath, "poke-card-image.htm"));

        if (PokeAPI.CardResults.Count > 0)
        {
            foreach (var pokeCard in PokeAPI.CardResults)
            {
                pokeCard.ImageHtml = Template.GetHtml(templateImg, pokeCard);

                html += Template.GetHtml(template, pokeCard);
            }
        }

        return html;
    }

    public static string ListCardTypes()
    {
        var html     = "";
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "menu-checkbox.htm"));

        foreach (var et in PC.SuperTypes)
        {
            var mc = new MenuCheckbox()
            {
                name  = et,
                title = et,
                color = "default",
            };
            html += Template.GetHtml(template, mc);
        }

        return html;
    }

    public static string ListRarities()
    {
        var html     = "";
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "menu-checkbox.htm"));

        foreach (var et in PC.Rarities)
        {
            var mc = new MenuCheckbox()
            {
                name  = et,
                title = et,
                color = "default",
            };
            html += Template.GetHtml(template, mc);
        }

        return html;
    }

    public static string ListElementTypes()
    {
        var html     = "";
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "menu-checkbox.htm"));

        foreach (var et in PC.ElementTypes)
        {
            var mc = new MenuCheckbox()
            {
                name  = et,
                title = et,
                color = "default",
            };
            html += Template.GetHtml(template, mc);
        }

        return html;
    }

    public static string ListSets()
    {
        var html     = "";
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "menu-checkbox.htm"));

        foreach (var et in PC.Sets)
        {
            var mc = new MenuCheckbox()
            {
                name  = et.id,
                title = et.name,
                color = "default",
            };
            html += Template.GetHtml(template, mc);
        }

        return html;
    }

}
