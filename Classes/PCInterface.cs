using System.Collections.Generic;
using System.IO;

namespace PokeCard;

public static class PCInterface
{
    public static async void ExecuteSearch(Dictionary<string, string> data)
    {
        Browser.FireJS(@"jFetch('search-results.app', '.page-content');");

        _ = await PokeAPI.CardSearch(data["query"]);

        Browser.FireJS(@"jFetchCards('render-cards.app', '#pcc-search');");

        Browser.FireJS(@"$('#searchSpinner').hide();");
        Browser.FireJS(string.Format("$('#searchText').html('<b>{0:N0}</b> results for: <b>&quot;{1}&quot;</b>');", PokeAPI.CardResults.Count, data["query"]));
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

    public static string ListFolders()
    {
        var html = "<li><div><a href=\"javascript:;\" data-bs-toggle=\"modal\" data-bs-target=\"#createFolderModal\" class=\"text-info\"><i class='bx bx-folder-plus text-info'></i>Create Folder</a></div></li>";
        
        var template = File.ReadAllText(Utils.Path(Config.WidgetPath, "menu-folder.htm"));

        var folderType = "";


        foreach (var et in PC.Folders)
        {
            if (folderType != et.folderType)
            {
                html += "<li class=\"dropdown-divider\"></li>";
                html += "<li class=\"dropdown-header\">[ <span>" + et.folderType + "</span> ]</li>";
                folderType = et.folderType;
            }


            html += Template.GetHtml(template, et);
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

    public static string CreateFolder(Dictionary<string, string> data)
    {

        var folder = new FolderData()
        {
            name       = data["folderName"],
            folderType = data["folderType"],
        };

        var addSuccess = PC.AddFolder(folder);

        if (addSuccess)
        {
            return "{\"status\":\"success\",\"msg\":\"Folder created successfully.\"}";
        }
        else
        {
            return "{\"status\":\"fail\",\"msg\":\"Folder already exists.\"}";
        }

    }


}
