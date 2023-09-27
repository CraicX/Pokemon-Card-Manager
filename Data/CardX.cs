﻿using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;

namespace PokeCardManager.Data;
public class CardX : Card
{
    public string ImageSmall => Images.Small.ToString();
    public string ImageLarge => Images.Large.ToString();
    public string SetName => Set.Name;

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

            if (decimal.TryParse(marketPrice, null, out var resPrice))
            {

                return string.Format("{0:C2}", resPrice);

            }
            else return "";

        }

    }

    public string RarityEffect => Rarity?.ToLower();
    public string SubtypeEffect => (Subtypes != null && Subtypes.Count > 0) ? string.Join(' ', Subtypes).ToLower() : "";

    public string ImageHtml
    {
        get; set;
    }
}