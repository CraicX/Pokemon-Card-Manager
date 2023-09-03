using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SuperTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace PokeCard;

public class CardX : Card
{
    public string ImageSmall => Images.Small.ToString();
    public string ImageLarge => Images.Large.ToString();
    public string SetName => Set.Name;
    public string Pricex => "$" + Tcgplayer.Prices.Normal.Market.ToString();
    public string Price
    {
        get
        {
            var marketPrice = "";

            if (Tcgplayer != null && Tcgplayer.Prices != null)
            {
                if (Tcgplayer.Prices.Holofoil != null)
                {
                    marketPrice = Tcgplayer.Prices.Holofoil.Market.ToString();
                }
                else if (Tcgplayer.Prices.ReverseHolofoil != null)
                {
                    marketPrice = Tcgplayer.Prices.ReverseHolofoil.Market.ToString();
                }
                else if (Tcgplayer.Prices.Normal != null)
                {
                    marketPrice = Tcgplayer.Prices.Normal.Market.ToString();
                }
            }
            if (marketPrice == "" && Cardmarket?.Prices != null)
            {
                marketPrice = Cardmarket.Prices.TrendPrice.ToString();
            }

            return "$" + marketPrice;

        }
       
    }

    public string RarityEffect => Rarity?.ToLower();

    public string ImageHtml {
        get; set;
    }
}

public static class PokeAPI
{

    static readonly string ApiKey = "3e058698-4207-4151-a9ba-c1973a1514df";

    public static PokemonApiClient pokeClient;

    public static SubTypes SubTypes;

    public static SuperTypes SuperTypes;

    public static Rarities Rarities;
    
    public static ElementTypes ElementTypes;

    static Dictionary<string, string> Filter = new();

    public static List<CardX> CardResults = new();
    public static List<Set> CardSets      = new();


    public static string[] PokemonSubTypes;



    public static void Init()
    {
        pokeClient = new PokemonApiClient(ApiKey);
        
        
    }

    public static async Task<string[]> GetSubTypes()
    {
        var types = await pokeClient.GetStringResourceAsync<SubTypes>();

        Debug.WriteLine(types.SubType.FirstOrDefault());

        PokemonSubTypes = types.SubType.ToArray();

        return types.SubType.ToArray();
    }

    public static async Task<List<PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set.Set>> GetSetsFromAPI()
    {
        var cardSets = await pokeClient.GetApiResourceAsync<PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set.Set>();

        CardSets = cardSets.Results;

        return CardSets;
    }


    public static async Task<List<CardX>> CardSearch(string query)
    {
        Filter.Clear();
        CardResults.Clear();

        Debug.WriteLine("Search Query: " + query);

        //  split query into words
        var words = query.Split(' ');

        if (PokemonSubTypes == null || PokemonSubTypes.Length == 0)
        {
            await GetSubTypes();
        }

        //  loop through words
        foreach (var word in words)
        {
            Debug.WriteLine("Search Word: " + word);
            if (PokemonSubTypes.Contains(word))
            {
                Filter.Add("subtypes", word);
            }
            else
            {
                Filter.Add("name", word);
            }
        }
        

        var cards = await pokeClient.GetApiResourceAsync<Card>(Filter);

        if (cards.Results.Count > 0)
        {
            Debug.WriteLine("Results found: " + cards.Results.Count);
            CardResults.Clear();

            foreach (var card in cards.Results)
            {
                // change to CardX
                CardX xcard = new();
                xcard.Map(card);

                CardResults.Add(xcard);
                
            }

        }

        return CardResults;

    }





}
