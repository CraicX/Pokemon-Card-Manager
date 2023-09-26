
using System.Collections.Generic;

namespace PokeCardManager.Data;
public class CardData
{
    public int rowId { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string supertype { get; set; }

    public List<string> subTypes { get; set; }

    public int setId { get; set; }

    public int number { get; set; }

    public string rarity { get; set; }

    public string imgSmall { get; set; }

    public string imgLarge { get; set; }

    public string tcgUrl { get; set; }

    public string tcgUrlReal { get; set; }

    public string cmUrl { get; set; }

    public string apiJson { get; set; }
}
