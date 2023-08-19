﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.SubTypes;
using PokemonTcgSdk.Standard.Features.FilterBuilder;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Base;
using System.Diagnostics;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards.Models;
using CefSharp.DevTools.CSS;
using System.Windows.Documents;
using System.Reflection;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace PokeCard;

public class CardX : Card
{
    public string ImageSmall => Images.Small.ToString();
    public string ImageLarge => Images.Large.ToString();

    
}

public static class PokeAPI
{

    static readonly string ApiKey = "3e058698-4207-4151-a9ba-c1973a1514df";

    public static PokemonApiClient pokeClient;

    public static SubTypes SubTypes;

    static Dictionary<string, string> Filter = new();

    public static List<CardX> CardResults = new();

    public static string[] PokemonSubTypes;



    public static void Init()
    {
        pokeClient = new PokemonApiClient(ApiKey);
        
        var _card = new CardX()
        {
            Name                 = "Pikachu",
            Id                   = "xy7-54",
            Images               = new CardImage()
            {
                Small = new System.Uri("https://images.pokemontcg.io/xy7/54.png"),
                Large = new System.Uri("https://images.pokemontcg.io/xy7/54_hires.png")
            },
            Types                = new List<string>() { "Lightning" },
            Supertype            = "Pokémon",
            Subtypes             = new List<string>() { "Basic" },
            Hp                   = 70,
            RetreatCost          = new List<string>() { "Colorless" },
            ConvertedRetreatCost = 1,
            Number               = "54",
            Artist               = "Kouki Saitou",
            Rarity               = "Uncommon",
        };

        CardResults.Add(_card);
    }

    public static async Task<string[]> GetSubTypes()
    {
        var types = await pokeClient.GetStringResourceAsync<SubTypes>();

        Debug.WriteLine(types.SubType.FirstOrDefault());

        PokemonSubTypes = types.SubType.ToArray();

        return types.SubType.ToArray();
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
