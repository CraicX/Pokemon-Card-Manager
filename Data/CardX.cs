//   _____     _                        _____           _    _____                         
//  |  _  |___| |_ ___ _____ ___ ___   |   __|___ ___ _| |  |     |___ ___ ___ ___ ___ ___ 
//  |   __| . | '_| -_|     | . |   |  |  |__  .'|  _| . |  | | | | .'|   | .'| . | -_|  _|
//  |__|  |___|_,_|___|_|_|_|___|_|_|  |_____|__,|_| |___|  |_|_|_|__,|_|_|__,|_  |___|_|  
//                                                                            |___|        
//  CardX
//
using Microsoft.VisualBasic;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;
using System.Reflection;

namespace PokeCardManager.Data;
public class CardX : Card
{
    public string ImageSmall    => Images.Small.ToString();
    public string ImageLarge    => Images.Large.ToString();
    public string SetName       => Set.Name;
    public string RarityEffect  => Rarity?.ToLower();
    public string SubtypeEffect => (Subtypes != null && Subtypes.Count > 0) ? string.Join(' ', Subtypes).ToLower() : "";
    public string ImageHtml { get; set; }

    public string TcgUrl => (Tcgplayer != null && Tcgplayer.Url != null) ? Tcgplayer.Url.ToString() : "";


    public string Safe(string propName)
    {
        if (this.GetType().GetProperty(propName) != null )
        {
            return this.GetType().GetProperty(propName).GetValue(this, null)?.ToString() ?? "";
        }
        return "";
    }


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

}