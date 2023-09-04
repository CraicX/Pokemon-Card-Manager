using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeCard;

// [rowId]           INTEGER NOT NULL PRIMARY KEY,
// [id]              VARCHAR(64)  NOT NULL DEFAULT '',
// [name]            VARCHAR(128) NOT NULL DEFAULT '',
// [series]          VARCHAR(128) NOT NULL DEFAULT '',
// [printedTotal]    INTEGER NOT NULL DEFAULT 0,
// [Total]           INTEGER NOT NULL DEFAULT 0,
// [releaseDate]     DATE NOT NULL DEFAULT '0000-00-00',
// [imgSymbol]       VARCHAR(200) NOT NULL DEFAULT '',
// [imgLogo]         VARCHAR(200) NOT NULL DEFAULT ''

[Table("Sets")]
public class SetData
{
    [Column("rowId")]
    public int rowId { get; set; }

    [Column("id")]
    public string id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("series")]
    public string series { get; set; }

    [Column("printedTotal")]
    public int printedTotal { get; set; }

    [Column("Total")]
    public int Total { get; set; }

    [Column("releaseDate")]
    public DateTime releaseDate { get; set; }

    [Column("imgSymbol")]
    public string imgSymbol { get; set; }

    [Column("imgLogo")]
    public string imgLogo { get; set; }


}
