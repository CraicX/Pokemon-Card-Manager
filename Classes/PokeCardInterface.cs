using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeCard;
public static class PokeCardInterface
{

    public static async void ExecuteSearch(Dictionary<string, string> data)
    {

        var cardz = await PokeAPI.CardSearch(data["query"]);

        Browser.FireJS(@"jFetch('render-cards.app', '#pcc-search');");


        //return PokeCardGui.RenderCards();

    }


}
