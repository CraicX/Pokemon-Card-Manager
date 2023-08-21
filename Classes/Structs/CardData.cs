using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeCard;

/*
 * 	[rowId]	       INTEGER      NOT NULL PRIMARY KEY,
	[id]	       VARCHAR(64)  NOT NULL DEFAULT '',
	[name]         VARCHAR(128) NOT NULL DEFAULT '',
	[supertype]    VARCHAR(128) NOT NULL DEFAULT '',
	[setId]        INTEGER      NOT NULL DEFAULT 0,
	[number]	   INTEGER      NOT NULL DEFAULT 0,
	[rarity]	   VARCHAR(64)  NOT NULL DEFAULT '',
	[imgSmall]	   VARCHAR(200) NOT NULL DEFAULT '',
	[imgLarge]     VARCHAR(200) NOT NULL DEFAULT '',
	[tcgUrl]	   VARCHAR(200) NOT NULL DEFAULT '',
	[tcgUrlReal]   VARCHAR(200) NOT NULL DEFAULT '',
	[cmUrl]	       VARCHAR(200) NOT NULL DEFAULT '',
	[apiJson]	   JSON         NOT NULL DEFAULT ''
*/

[Table("Cards")]
public class CardData
{
    [Column("rowId")]
    public int rowId { get; set; }

    [Column("id")]
    public string id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("supertype")]
    public string supertype { get; set; }

    public List<string> subTypes { get; set; }

    [Column("setId")]
    public int setId { get; set; }

    [Column("number")]
    public int number { get; set; }

    [Column("rarity")]
    public string rarity { get; set; }

    [Column("imgSmall")]
    public string imgSmall { get; set; }

    [Column("imgLarge")]
    public string imgLarge { get; set; }

    [Column("tcgUrl")]
    public string tcgUrl { get; set; }

    [Column("tcgUrlReal")]
    public string tcgUrlReal { get; set; }

    [Column("cmUrl")]
    public string cmUrl { get; set; }

    [Column("apiJson")]
    public string apiJson { get; set; }
}
