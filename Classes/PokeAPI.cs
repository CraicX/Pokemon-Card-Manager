using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Rarities;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SuperTypes;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Types;


namespace PokeCard;

public static class PokeAPI
{
    static readonly string ApiKey = "3e058698-4207-4151-a9ba-c1973a1514df";

    public static PokemonApiClient pokeClient;
    public static SubTypes SubTypes;
    public static SuperTypes SuperTypes;
    public static Rarities Rarities;
    public static ElementTypes ElementTypes;

    static Dictionary<string, List<string>> Filter = new();
    
    public static List<CardX> CardResults          = new();
    public static List<Set> CardSets               = new();


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

        //  split query into words
        var words = query.Split(' ');

        //if (PokemonSubTypes == null || PokemonSubTypes.Length == 0)
        //{
        //    await GetSubTypes();
        //}

        //  loop through words
        foreach (var word in words)
        {
            if (PC.SubTypes.Contains(word, StringComparer.OrdinalIgnoreCase))
            {
                if (!Filter.ContainsKey("subtypes")) Filter.Add("subtypes", new List<string>());

                Filter["subtypes"].Add(word);
            }
            else
            {
                if (!Filter.ContainsKey("name") ) Filter.Add("name", new List<string>());

                Filter["name"].Add(word);
            }
        }

        var searchFilter = BuildFilterDict();

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };


        var cards = await pokeClient.GetApiResourceAsync<Card>(searchFilter);

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

}
