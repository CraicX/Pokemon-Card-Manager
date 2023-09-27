//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  PokeAPI
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeCardManager.Data;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SuperTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;

namespace PokeCardManager.Classes;
public static class PokeAPI
{
    static readonly string ApiKey = "3e058698-4207-4151-a9ba-c1973a1514df";

    public static PokemonApiClient PokeClient;
    public static SubTypes SubTypes;
    public static SuperTypes SuperTypes;
    public static Rarities Rarities;
    public static ElementTypes ElementTypes;

    static readonly Dictionary<string, List<string>> Filter = new();

    public static List<CardX> CardResults = new();
    public static List<Set> CardSets      = new();
    public static string Query            = string.Empty;
    public static ResultSet ResultSet     = new();

    public static string[] PokemonSubTypes;


    //
    // ─── INIT ────────────────────────────────────────────────────────────────
    //
    public static void Init()
    {
        PokeClient = new PokemonApiClient(ApiKey);
    }


    public static async Task<string[]> GetSubTypes()
    {
        var types = await PokeClient.GetStringResourceAsync<SubTypes>();
        PokemonSubTypes  = types.SubType.ToArray();
        return PokemonSubTypes;
    }


    public static async Task<List<Set>> GetSetsFromAPI()
    {
        var cardSetsResponse = await PokeClient.GetApiResourceAsync<Set>();
        CardSets = cardSetsResponse.Results;
        return CardSets;
    }

   
    public static Dictionary<string, string> BuildFilterDict()
    {
        var dict = new Dictionary<string, string>();

        foreach (var item in Filter)
        {
            if (item.Value.Count > 0)
            {
                dict.Add(item.Key, string.Join(';', item.Value));
            }
        }

        return dict;
    }


    private static void AddToFilter(string key, string value)
    {
        if (!Filter.ContainsKey(key))
        {
            Filter.Add(key, new List<string>());
        }

        Filter[key].Add(value.ToLower());
    }


    //
    // ─── CARD SEARCH ────────────────────────────────────────────────────────────────
    //
    public static async Task<List<CardX>> CardSearch(string query="")
    {
        
        Filter.Clear();
        CardResults.Clear();

        if (query == null || query == string.Empty) query = Query;

        if (query != string.Empty) { 
        
            var words = query.Split(' ');

            foreach (var word in words)
            {
                if (PC.SubTypes.Contains(word, StringComparer.OrdinalIgnoreCase))
                {
                    AddToFilter("subtypes", word);
                }
                else
                {
                    AddToFilter("name", word);
                }
            }

        }

        if (PC.Filters.Count >= 1)
        {
            foreach (var filter in PC.Filters)
            {
                if (filter.Type == Data.Filter.FilterTypes.SubTypes) 
                {
                    AddToFilter("subtypes", filter.Value);
                }
                else if (filter.Type == Data.Filter.FilterTypes.Rarities)
                {
                    AddToFilter("rarity", filter.Value);
                }
                else if (filter.Type == Data.Filter.FilterTypes.ElementTypes)
                {
                    AddToFilter("types", filter.Value);
                }
                else if (filter.Type == Data.Filter.FilterTypes.SetName)
                {
                    AddToFilter("set.id", filter.Value);
                }
            }
        }

        var searchFilter = BuildFilterDict();
        var cards = await PokeClient.GetApiResourceAsync<Card>(searchFilter);


        ResultSet = new()
        {
            Count      = cards.Count,
            FromCache  = cards.FromCache,
            Page       = int.Parse(cards.Page),
            PageSize   = int.Parse(cards.PageSize),
            TotalCount = cards.TotalCount,
        };

        if (cards.Results.Count > 0)
        {
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
