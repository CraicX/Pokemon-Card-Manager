using System.ComponentModel.DataAnnotations.Schema;

namespace PokeCardManager.Data;
public class SetData
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

    [Column("series")]
    public string series
    {
        get; set;
    }

    [Column("printedTotal")]
    public int printedTotal
    {
        get; set;
    }

    [Column("Total")]
    public int Total
    {
        get; set;
    }

    [Column("releaseDate")]
    public DateTime releaseDate
    {
        get; set;
    }

    [Column("imgSymbol")]
    public string imgSymbol
    {
        get; set;
    }

    [Column("imgLogo")]
    public string imgLogo
    {
        get; set;
    }


}
