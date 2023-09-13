using System.ComponentModel.DataAnnotations.Schema;

namespace PokeCardManager.Data;
public class CardData
{
    [Column("rowId")]
    public int rowId
    {
        get; set;
    }

    [Column("id")]
    public string id
    {
        get; set;
    }

    [Column("name")]
    public string name
    {
        get; set;
    }

    [Column("supertype")]
    public string supertype
    {
        get; set;
    }

    public List<string> subTypes
    {
        get; set;
    }

    [Column("setId")]
    public int setId
    {
        get; set;
    }

    [Column("number")]
    public int number
    {
        get; set;
    }

    [Column("rarity")]
    public string rarity
    {
        get; set;
    }

    [Column("imgSmall")]
    public string imgSmall
    {
        get; set;
    }

    [Column("imgLarge")]
    public string imgLarge
    {
        get; set;
    }

    [Column("tcgUrl")]
    public string tcgUrl
    {
        get; set;
    }

    [Column("tcgUrlReal")]
    public string tcgUrlReal
    {
        get; set;
    }

    [Column("cmUrl")]
    public string cmUrl
    {
        get; set;
    }

    [Column("apiJson")]
    public string apiJson
    {
        get; set;
    }
}
